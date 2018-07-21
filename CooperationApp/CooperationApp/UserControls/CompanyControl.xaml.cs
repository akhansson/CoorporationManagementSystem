﻿using CooperationApp.Data;
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
    public partial class CompanyControl : UserControl
    {
        private CompanyService _companyService;

        public CompanyControl()
        {
            _companyService = new CompanyService();

            InitializeComponent();
        }

        private void saveCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a Company object
                _companyService.AddCompany(new Company()
                {
                    CompanyName = companyNameTexbox.Text
                });
                companyNameTexbox.Text = null;

                setCompanyAmountLabel();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        

        public void showCompaniesButton_Click(object sender, RoutedEventArgs e)
        {
            var companiesWindow = new DisplayCompanies();
            companiesWindow.ShowDialog();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            setCompanyAmountLabel();
        }

        private void setCompanyAmountLabel()
        {
            companyAmountLabel.Content = _companyService.CompanyAmount();
        }
    }
}
