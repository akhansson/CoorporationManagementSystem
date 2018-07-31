using CooperationApp.Models;
using CooperationApp.Services;
using Core.Util;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CooperationApp.UserControls
{
    /// <summary>
    /// Interaction logic for PersonControl.xaml
    /// </summary>
    public partial class PersonControl : UserControl
    {
        private CompanyService _companyService;
        private PersonService _personService;

        public PersonControl()
        {
            _companyService = new CompanyService();
            _personService = new PersonService();
            
            InitializeComponent();
            
        }

        private void savePersonButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedCompany = companyCombobox.SelectedItem as Company;
                _personService.AddPerson(new Person
                {
                    FullName = nameTextBox.Text,
                    CompanyId = selectedCompany?.Id
                });

                nameTextBox.Text = null;
                setPeopleAmountLabel();
            }
            catch (ArgumentException ex)
            {
                nameTextBox.Focus();
                nameTextBox.SelectAll();
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void setPeopleAmountLabel()
        {
            peopleAmountLabel.Content = _personService.PeopleAmount();
        }

        public void PopulateCompanyComboBox()
        {
            var companies = _companyService.GetAllCompanies();
            companyCombobox.ItemsSource = companies;
            companyCombobox.SelectedIndex = -1;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateCompanyComboBox();
            setPeopleAmountLabel();
        }

        private void viewPeopleButton_Click(object sender, RoutedEventArgs e)
        {
            var displayPeople = new DisplayPeople();

            // add subscriber method to the event
            displayPeople.PersonDeleted += OnPersonDeleted;

            displayPeople.ShowDialog();
        }

        // Subscriber to the PersonDeleted event
        public void OnPersonDeleted(object source, EventArgs e)
        {
            setPeopleAmountLabel();
        }
    }
}
