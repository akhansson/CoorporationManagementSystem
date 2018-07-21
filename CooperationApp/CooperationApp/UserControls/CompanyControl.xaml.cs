using CooperationApp.Coorperation;
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
            }
            // This code runs if the company name provided is composed of letters. No other characters are accepted.
            else if (Regex.IsMatch(companyNameTexbox.Text, @"^[A-Za-zÅÄÖåäö ]+$"))
            {
                // Create a Company object
                Company person = new Company()
                {
                    CompanyName = companyNameTexbox.Text
                };
                companyNameTexbox.Text = null;
            }
            else
            {
                MessageBox.Show("Error: You should only provide letters in the company name. No other characters are supported!", "Non letter characters were provided", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
