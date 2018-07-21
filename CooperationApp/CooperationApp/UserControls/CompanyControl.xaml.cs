﻿using CooperationApp.Coorperation;
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
        public CompanyControl()
        {
            InitializeComponent();
        }

        private void saveCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            // Outputs an error if the company name provided is null or an empty string or empty characters
            if (string.IsNullOrWhiteSpace(companyNameTexbox.Text))
            {
                MessageBox.Show("You didn't write anything in the field!", "Error: No company name written", MessageBoxButton.OK, MessageBoxImage.Error);
                companyNameTexbox.Text = null;
            }
            // This code runs if the company name provided is composed of letters. No other characters are accepted.
            else if (Regex.IsMatch(companyNameTexbox.Text, @"^[A-Za-zÅÄÖåäö ]+$"))
            {
                // Execute if companyNameTextbox.Text isn't found in the database
                if (true)
                {
                    // Create a Company object
                    Company company = new Company()
                    {
                        CompanyName = companyNameTexbox.Text
                    };

                    // The path of the database
                    string databaseName = "Companies.db";
                    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string databasePath = System.IO.Path.Combine(folderPath, databaseName);

                    // Initialize the SQLite connection
                    // The "using" statement takes care of closing the connection"
                    using (SQLiteConnection connection = new SQLiteConnection(databasePath))
                    {
                        // Create a table of the type Person
                        connection.CreateTable<Company>();
                        // Insert the person object that was created when the save button was clicked into the SQLite table.
                        connection.Insert(company);
                    }

                    companyNameTexbox.Text = null;
                }
                // Execute if companyNameTextbox.Text is found in the database. Display error message!
                else
                {
                    MessageBox.Show("The company already exists in the company database.", "Error: Company already exists", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Error: You should only provide letters in the company name. No other characters are supported!", "Non letter characters were provided", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
