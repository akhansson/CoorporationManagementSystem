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
    /// Interaction logic for PersonControl.xaml
    /// </summary>
    public partial class PersonControl : UserControl
    {
        private CompanyService _companyService;

        public PersonControl()
        {
            _companyService = new CompanyService();

            InitializeComponent();
        }

        private void savePersonButton_Click(object sender, RoutedEventArgs e)
        {
            // If the company checkbox is unchecked
            if (isEmployedCheckbox.IsChecked == false)
            {

            }
            // If the company checkbox is checked
            else
            {
                
            }
        }

        public void ReadCompanyDatabase()
        {
            var companies = _companyService.GetAllCompanies();

            foreach (var company in companies)
            {
                companyCombobox.Items.Add(new ComboBoxItem()
                {
                    Content = company.CompanyName
                });
            }
        }

        private void isEmployedCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (!companyCombobox.HasItems)
            {
                MessageBox.Show("There are no companies registered. Register a company in the company tab first if you plan to add the person to a company.", "No companies in the database", MessageBoxButton.OK, MessageBoxImage.Error);
                isEmployedCheckbox.IsChecked = false;
            }
            else
            {
                companyCombobox.IsEnabled = true;
            }
        }

        private void isEmployedCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            companyCombobox.IsEnabled = false;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ReadCompanyDatabase();
        }
    }
}
