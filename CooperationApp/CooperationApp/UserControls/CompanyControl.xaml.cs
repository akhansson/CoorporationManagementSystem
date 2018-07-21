using CooperationApp.Coorperation;
using CooperationApp.Data;
using CooperationApp.Models;
using CooperationApp.Services;
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
    public partial class CompanyControl : UserControl, ICompanyDatabase
    {
        private CompanyService _companyService;

        public CompanyControl()
        {
            InitializeComponent();

            ReadCompanyDatabase();

            _companyService = new CompanyService();

        }

        private void saveCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(companyNameTexbox.Text))
            {
                MessageBox.Show("You didn't write anything in the field!", "Error: No company name written", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                // Create a Company object
                _companyService.AddCompany(new Company()
                {
                    CompanyName = companyNameTexbox.Text
                });
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            //// Outputs an error if the company name provided is null or an empty string or empty characters
            //if (string.IsNullOrWhiteSpace(companyNameTexbox.Text))
            //{
            //    MessageBox.Show("You didn't write anything in the field!", "Error: No company name written", MessageBoxButton.OK, MessageBoxImage.Error);
            //    companyNameTexbox.Text = null;
            //}
            //// This code runs if the company name provided is composed of letters. No other characters are accepted.
            //else if (Regex.IsMatch(companyNameTexbox.Text, @"^[A-Za-zÅÄÖåäö ]+$"))
            //{
            //    // Execute if companyNameTextbox.Text isn't found in the database
            //    if (true)
            //    {


            //        companyNameTexbox.Text = null;
            //        ReadCompanyDatabase();
            //    }
            //    // Execute if companyNameTextbox.Text is found in the database. Display error message!
            //    else
            //    {
            //        MessageBox.Show("The company already exists in the company database.", "Error: Company already exists", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Error: You should only provide letters in the company name. No other characters are supported!", "Non letter characters were provided", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }



        public void ReadCompanyDatabase()
        {
            var companies = _companyService.GetAllCompanies();

            companyAmountLabel.Content = $"{companies.Count} companies in the database";
        }

        public void showCompaniesButton_Click(object sender, RoutedEventArgs e)
        {
            var companiesWindow = new DisplayCompanies();
            companiesWindow.ShowDialog();
        }
    }
}
