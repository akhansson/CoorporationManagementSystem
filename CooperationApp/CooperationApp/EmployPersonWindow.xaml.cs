using System;
using CooperationApp.Services;
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
using CooperationApp.Models;

namespace CooperationApp
{
    /// <summary>
    /// Interaction logic for EmployPersonWindow.xaml
    /// </summary>
    public partial class EmployPersonWindow : Window
    {
        public delegate void OnPersonEmployedEventHandler(object source, EventArgs e);
        public event OnPersonEmployedEventHandler PersonEmployed;

        Person selectedPerson;

        private CompanyService _companyService;
        private PersonService _personService;

        public EmployPersonWindow(Person personName)
        {
            _companyService = new CompanyService();
            _personService = new PersonService();
            InitializeComponent();
            selectedPerson = personName;
            personNameLabel.Content = selectedPerson.FullName;
        }

        public void PopulateCompaniesComboBox()
        {
            var companies = _companyService.GetAllCompanies();
            employComboBox.ItemsSource = companies;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateCompaniesComboBox();
            employComboBox.SelectedIndex = 0;
        }

        private void employButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                var selectedCompany = employComboBox.SelectedItem as Company;

                _personService.EmployPerson(selectedPerson.Id, selectedCompany.Id);

                OnPersonEmployed();
                employButton.IsEnabled = false;

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public virtual void OnPersonEmployed()
        {
            if (PersonEmployed != null)
                PersonEmployed(this, EventArgs.Empty);
        }

        private void employComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            employButton.IsEnabled = true;
        }
    }
}
