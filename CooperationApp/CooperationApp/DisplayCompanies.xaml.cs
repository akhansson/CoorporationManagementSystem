﻿using CooperationApp.Models;
using CooperationApp.Services;
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
        private CompanyService _companyService;
        
        public DisplayCompanies()
        {
            _companyService = new CompanyService();
            InitializeComponent();
            DisplayCompaniesFromDatabase();
        }

        public void DisplayCompaniesFromDatabase()
        {
            var companies = _companyService.GetAllCompanies();
            companiesListView.ItemsSource = companies;
        }
        

        private void deleteCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCompany = companiesListView.SelectedItem as Company;

            _companyService.RemoveCompany(selectedCompany);
            DisplayCompaniesFromDatabase();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var companies = _companyService.GetAllCompanies();

            var searchTextBox = sender as TextBox;
            var filteredList = companies.Where(c => c.CompanyName.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();

            companiesListView.ItemsSource = filteredList;
        }
    }
}
