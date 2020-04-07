using Existek_WPF_Part.Common;
using Existek_WPF_Part.Models;
using Existek_WPF_Part.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Existek_WPF_Part.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private Customer _selectedCustomer;
        private string _searchString;

        public DelegateCommand NewCustomerCommand { get; set; }
        public DelegateCommand EditCustomerCommand { get; set; }
        public DelegateCommand ReloadCustomersCommand { get; set; }
        public DelegateCommand RemoveCustomerCommand { get; set; }
        public DelegateCommand SearchCustomerCommand { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }
        public Customer SelectedCustomer
        { 
            get => _selectedCustomer;
            set 
            {
                if (!SetValue(ref _selectedCustomer, value)) return;
                _selectedCustomer = value;
                OnPropertyChanged();
            } 
        }

        public string SearchString 
        { 
            get => _searchString; 
            set
            {
                if (!SetValue(ref _searchString, value)) return;
                _searchString = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            NewCustomerCommand = new DelegateCommand(OnNewCustomer);
            EditCustomerCommand = new DelegateCommand(OnEditCustomer, CanEditCustomer);
            ReloadCustomersCommand = new DelegateCommand(OnReloadCustomers);
            RemoveCustomerCommand = new DelegateCommand(OnRemoveCustomer, CanRemoveCustomer);
            SearchCustomerCommand = new DelegateCommand(OnSearchCustomer, CanSearchCustomer);
            Customers = new ObservableCollection<Customer>();
            LoadDataAsync();
            SelectedCustomer = Customers.FirstOrDefault();
        }

        private bool CanSearchCustomer(object obj)
        {
            return !string.IsNullOrWhiteSpace(SearchString);
        }

        private void OnSearchCustomer(object obj)
        {
            foreach (var customer in Customers)
            {
                if (customer.FName.Contains(SearchString) || customer.LName.Contains(SearchString) || 
                    customer.Cpny.Contains(SearchString) || customer.City.Contains(SearchString))
                {
                    SelectedCustomer = customer;
                    return;
                }
            }
        }
        //retrieve data from ASP.NET Core project
        static HttpClient client = new HttpClient();
        string path = "http://localhost:3307/api/customers";
        public async Task RunAsync(string path)
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri(path);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = (await client.GetAsync(path)).EnsureSuccessStatusCode();
            var responceBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Customer>>(responceBody);
            foreach (var item in result)
            {
                Customers.Add(item);
            }
            //if (response.IsSuccessStatusCode)
            //{
            //    IEnumerable<Customer> customers = await response.Content.ReadAsAsync<IEnumerable<Customer>>();
            //}
        }
            private async void LoadDataAsync()
        {
            Customers.Clear();
            await RunAsync(path);
        }

        private bool CanRemoveCustomer(object obj)
        {
            return SelectedCustomer != null; ;
        }

        private void OnRemoveCustomer(object obj)
        {
           Customers.Remove(SelectedCustomer);
        }

        private void OnReloadCustomers(object obj)
        {
            LoadDataAsync();
        }

        private bool CanEditCustomer(object obj)
        {
            return SelectedCustomer != null;
        }

        private void OnEditCustomer(object obj)
        {
            var editCustomerWindow = new NewEditPersonView(SelectedCustomer).ShowDialog();
        }

        private void OnNewCustomer(object obj)
        {
            var editCustomerWindow = new NewEditPersonView(new Customer {FirstContact = DateTime.Now }).ShowDialog();
        }
    }
}
