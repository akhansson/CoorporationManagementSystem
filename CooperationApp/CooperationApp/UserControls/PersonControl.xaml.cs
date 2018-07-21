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
    /// Interaction logic for PersonControl.xaml
    /// </summary>
    public partial class PersonControl : UserControl
    {
        public PersonControl()
        {
            InitializeComponent();
        }

        private void savePersonButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTexbox.Text))
            {
                MessageBox.Show("Could not add the person to the database. You have to provide a name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (companyCombobox.SelectedItem != null)
                {
                    MessageBox.Show(companyCombobox.SelectedItem.ToString());
                    
                }
            }
        }

        private void isEmployedCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            companyCombobox.IsEnabled = true;
        }

        private void isEmployedCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            companyCombobox.IsEnabled = false;
        }
    }
}
