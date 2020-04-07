using Existek_WPF_Part.Common;
using Existek_WPF_Part.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Existek_WPF_Part.ViewModels
{
    public class NewEditContactsViewModel : BaseViewModel
    {
        private CustomerContact currentContact;

        public CustomerContact CurrentContact
        {
            get => currentContact;
            set
            {
                if (!SetValue(ref currentContact, value)) return;
                currentContact = value;
                OnPropertyChanged();
            }
        }
        public DelegateCommand SaveContactCommand { get; set; }
        public DelegateCommand CancelContactCommand { get; set; }

        public NewEditContactsViewModel(CustomerContact customerContact)
        {
            CurrentContact = customerContact;
            SaveContactCommand = new DelegateCommand(OnSaveContact, CanSaveContact);
            CancelContactCommand = new DelegateCommand(OnCancelContact);
        }

        private void OnCancelContact(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
            };
        }

        private bool CanSaveContact(object obj)
        {
            return (!string.IsNullOrWhiteSpace(CurrentContact.ContactType) && !string.IsNullOrWhiteSpace(CurrentContact.ContactTxt));
        }

        private void OnSaveContact(object obj)
        {
            if (obj is CustomerContact contact)
            {

            };
        }
    }
}
