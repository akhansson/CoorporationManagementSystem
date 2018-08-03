using CooperationApp.Models;
using CooperationApp.Services;
using Core.Util;
using SQLite;
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

namespace CooperationApp
{
    public class CompanyEventArgs : EventArgs
    {
        public List<Company> Company { get; set; }
    }

    public partial class DisplayCompanies : Window
    {
        public delegate void OnCompanyDeletedEventHandler(object source, CompanyEventArgs e);
        public event OnCompanyDeletedEventHandler CompanyDeleted;

        private CompanyService _companyService;

        public DisplayCompanies()
        {
            _companyService = new CompanyService();
            InitializeComponent();
        }

        private void DisplayCompaniesFromDatabase()
        {
            var companiesAndAmountOfEmployees = _companyService.AmountOfEmployees();
            companiesListView.ItemsSource = companiesAndAmountOfEmployees;
        }
        

        private void deleteCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCompanies = companiesListView.SelectedItems;

            List<Company> companiesToDelete = new List<Company>();

            foreach (var c in selectedCompanies)
            {
                var company1 = c as CompanyCount;

                companiesToDelete.Add(new Company { Id = company1.Id, CompanyName = company1.CompanyName });

            }

            _companyService.RemoveCompany(companiesToDelete);

            OnCompanyDeleted(companiesToDelete);

            DisplayCompaniesFromDatabase();

            
        }
        

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var companies = _companyService.SearchCompany(searchTextBox.Text);

           //var filteredList = companies.Where(c => c.CompanyName.IndexOf(searchTextBox.Text, StringComparison.CurrentCultureIgnoreCase) > -1).ToList();

            companiesListView.ItemsSource = companies;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayCompaniesFromDatabase();
        }

        public virtual void OnCompanyDeleted(List<Company> company)
        {
            if (CompanyDeleted != null)
            {
                CompanyDeleted(this, new CompanyEventArgs() { Company = company });
            }
        }
    }
}
