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
            if (string.IsNullOrWhiteSpace(companyNameTexbox.Text))
            {
                MessageBox.Show("You didn't write anything in the field!", "Error: No company name written", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
