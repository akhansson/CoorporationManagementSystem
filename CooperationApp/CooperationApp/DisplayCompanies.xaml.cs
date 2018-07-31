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
    /// <summary>
    /// Interaction logic for DisplayCompanies.xaml
    /// </summary>
    public partial class DisplayCompanies : Window
    {
        public delegate void OnCompanyDeletedEventHandler(object source, EventArgs e);
        public event OnCompanyDeletedEventHandler CompanyDeleted;

        private CompanyService _companyService;

        public DisplayCompanies()
        {
            _companyService = new CompanyService();
            InitializeComponent();
            DisplayCompaniesFromDatabase();
        }

        private void DisplayCompaniesFromDatabase()
        {
            var companies = _companyService.GetAllCompanies();
            companiesListView.ItemsSource = companies;
        }
        

        private void deleteCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCompany = companiesListView.SelectedItem as Company;
            
            _companyService.RemoveCompany(selectedCompany);

            DisplayCompaniesFromDatabase();

            OnCompanyDeleted();
        }
        

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var companies = _companyService.SearchCompany(searchTextBox.Text);

           //var filteredList = companies.Where(c => c.CompanyName.IndexOf(searchTextBox.Text, StringComparison.CurrentCultureIgnoreCase) > -1).ToList();

            companiesListView.ItemsSource = companies;
        }

        public virtual void OnCompanyDeleted()
        {
            if (CompanyDeleted != null)
            {
                CompanyDeleted(this, EventArgs.Empty);
            }
        }
    }
}
