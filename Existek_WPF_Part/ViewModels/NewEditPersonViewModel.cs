using Existek_WPF_Part.Common;
using Existek_WPF_Part.Models;
using Existek_WPF_Part.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Existek_WPF_Part.ViewModels
{
    public class NewEditPersonViewModel : BaseViewModel
    {
        private Customer _currentCustomer;
        private CustomerContact _currentCustomerSelectedContact;

        public Customer CurrentCustomer 
        { 
            get => _currentCustomer; 
            set
            {
                if (!SetValue(ref _currentCustomer, value)) return;
                _currentCustomer = value;
                OnPropertyChanged();
            }
        }

        public CustomerContact CurrentCustomerSelectedContact
        {
            get => _currentCustomerSelectedContact;
            set
            {
                if (!SetValue(ref _currentCustomerSelectedContact, value)) return;
                _currentCustomerSelectedContact = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand SaveCustomerCommand { get; set; }
        public DelegateCommand CancelCustomerCommand { get; set; }
        public DelegateCommand NewContactCommand { get; set; }
        public DelegateCommand EditContactCommand { get; set; }
        public DelegateCommand DeleteContactCommand { get; set; }

        public NewEditPersonViewModel(Customer customer)
        {
            CurrentCustomer = customer;
            CurrentCustomerSelectedContact = customer.CustomerContacts.FirstOrDefault();
            SaveCustomerCommand = new DelegateCommand(OnSaveCustomer, CanSaveCustomer);
            CancelCustomerCommand = new DelegateCommand(OnCancelCustomer);
            NewContactCommand = new DelegateCommand(OnNewContact);
            EditContactCommand = new DelegateCommand(OnEditContact);
            DeleteContactCommand = new DelegateCommand(OnDeleteContact, CanDeleteContact);
        }

        private bool CanDeleteContact(object obj)
        {
            return CurrentCustomerSelectedContact != null;
        }

        private void OnDeleteContact(object obj)
        {
           var contacts = CurrentCustomer.CustomerContacts.ToList();
            contacts.Remove(CurrentCustomerSelectedContact);
            CurrentCustomer.CustomerContacts = contacts.AsEnumerable();
        }

        private void OnEditContact(object obj)
        {
            var edit = new NewEditContactsView(CurrentCustomerSelectedContact);
        }

        private void OnNewContact(object obj)
        {
            var edit = new NewEditContactsView(new CustomerContact());
        }

        private void OnCancelCustomer(object obj)
        {
            if (obj is Window window)
            {
                CurrentCustomer = null;
                window.Close();
            }
        }

        private bool CanSaveCustomer(object obj)
        {
            return (!string.IsNullOrWhiteSpace(CurrentCustomer.FName) && !string.IsNullOrWhiteSpace(CurrentCustomer.LName)
                && !string.IsNullOrWhiteSpace(CurrentCustomer.CountryTxt) && !string.IsNullOrWhiteSpace(CurrentCustomer.GreetingTxt));
        }

        private void OnSaveCustomer(object obj)
        {
            if (obj is Customer customer)
            {
               
            }
        }
    }
}
