﻿using CooperationApp.Data;
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
    /// Interaction logic for CompanyControl.xaml
    /// </summary>
    public partial class CompanyControl : UserControl
    {
        private CompanyService _companyService;
        private PersonService _personService;

        public CompanyControl()
        {
            _companyService = new CompanyService();
            _personService = new PersonService();

            _companyService.Event.OnEvent += (object source, EventArgs e) =>
            {

                var evt = (EventClassArgs)e;
                if (evt.Name == "error")
                {
                    saveCompanyButton.IsEnabled = true;
                }
            };

            InitializeComponent();
        }

        public void PopulateCompaniesComboBox()
        {
            var companies = _companyService.GetAllCompanies();
            companyPickerComboBox.ItemsSource = companies;
        }

        public void DisplayEmployeesFromDatabase(Company company)
        {
            var employedPeople = _personService.GetEmployees(company);
            employeeListView.ItemsSource = employedPeople.Select(c => c.FullName);
        }

        private async void saveCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                saveCompanyButton.IsEnabled = false;
                // Create a Company object
                await _companyService.AddCompany(new Company()
                {
                    CompanyName = companyNameTexbox.Text
                });

                companyNameTexbox.Text = null;

                saveCompanyButton.IsEnabled = true;

                setCompanyAmountLabel();

                PopulateCompaniesComboBox();
            }
            catch (ArgumentException ex)
            {
                companyNameTexbox.Focus();
                companyNameTexbox.SelectAll();
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void showCompaniesButton_Click(object sender, RoutedEventArgs e)
        {
            var displayCompanies = new DisplayCompanies();

            displayCompanies.CompanyDeleted += OnCompanyDeleted;

            displayCompanies.ShowDialog();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateCompaniesComboBox();
            setCompanyAmountLabel();
        }

        private void setCompanyAmountLabel()
        {
            companyAmountLabel.Content = _companyService.CompanyAmount();
        }

        private void companyPickerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectedCompany = companyPickerComboBox.SelectedItem as Company;
                if (selectedCompany != null)
                {
                    DisplayEmployeesFromDatabase(selectedCompany);
                }
                else
                {
                    employeeListView.ItemsSource = null;
                }
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }

        public void OnCompanyDeleted(object source, CompanyEventArgs e)
        {
            setCompanyAmountLabel();

            var deletedCompany = e.Company;

            var selectedValue = companyPickerComboBox.SelectedValue;


            var selectedCompany = companyPickerComboBox.SelectedItem as Company;

            PopulateCompaniesComboBox();

            if (deletedCompany != selectedCompany)
            {
                if (selectedCompany != null)
                {
                    // Jag vill motverka att bomboboxen blir tom
                    // Jag vill att det valda objektet kvarstår
                    //companyPickerComboBox.SelectedItem = selectedCompany;
                    DisplayEmployeesFromDatabase(selectedCompany);
                }
                else
                {
                    employeeListView.ItemsSource = null;
                }
            }
        }
    }
}
