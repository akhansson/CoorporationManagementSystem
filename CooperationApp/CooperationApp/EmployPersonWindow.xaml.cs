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
using System.Collections;

namespace CooperationApp
{
    /// <summary>
    /// Interaction logic for EmployPersonWindow.xaml
    /// </summary>
    public partial class EmployPersonWindow : Window
    {
        public delegate void OnPersonEmployedEventHandler(object source, EventArgs e);
        public event OnPersonEmployedEventHandler PersonEmployed;

        string str;

        private CompanyService _companyService;
        private PersonService _personService;

        List<Person> people = new List<Person>();

        public EmployPersonWindow(IList personNames)
        {
            _companyService = new CompanyService();
            _personService = new PersonService();

            IList selectedPeople = personNames;

            InitializeComponent();

            

            foreach (var person in selectedPeople)
            {
                people.Add(person as Person);
            }

            int i = 0;
            foreach (var person in people)
            {
                if (i == 5)
                {
                    break;
                }
                str += person.FullName + ", ";
                i++;
            }
            str = str.Remove(str.Length - 2);

            if (people.Count > 5)
                str += "...";
            
            personNameLabel.Text = str;
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

                foreach (var person in people)
                {
                    _personService.EmployPerson(person.Id, (int)selectedCompany.Id);
                }

                OnPersonEmployed();
                this.Close();
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
    }
}
