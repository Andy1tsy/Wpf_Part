using Existek_WPF_Part.Models;
using Existek_WPF_Part.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Existek_WPF_Part.Views
{
    /// <summary>
    /// Interaction logic for NewEditContactsView.xaml
    /// </summary>
    public partial class NewEditContactsView : Window
    {
        private NewEditContactsViewModel _newEditContactsViewModel;
        private CustomerContact currentCustomerSelectedContact;

        public NewEditContactsView(CustomerContact currentCustomerSelectedContact)
        {
            InitializeComponent();
            _newEditContactsViewModel = new NewEditContactsViewModel(currentCustomerSelectedContact);
        }

        //public NewEditContactsView(CustomerContact currentCustomerSelectedContact)
        //{
        //    this.currentCustomerSelectedContact = currentCustomerSelectedContact;
        //}
    }
}
