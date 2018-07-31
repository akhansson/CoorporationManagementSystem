using System;
using CooperationApp.Services;
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
using CooperationApp.Models;

namespace CooperationApp.UserControls
{
    /// <summary>
    /// Interaction logic for UnemployedControl.xaml
    /// </summary>
    public partial class UnemployedControl : UserControl
    {
        
        private PersonService _personService;

        public UnemployedControl()
        {
            _personService = new PersonService();

            InitializeComponent();

            DisplayUnemployedFromDatabase();
        }

        public void DisplayUnemployedFromDatabase()
        {
            var unemployed = _personService.GetAllUnemployed();
            unemployedListView.ItemsSource = unemployed;
        }

        

        private void employButton_Click(object sender, RoutedEventArgs e)
        {
            if (unemployedListView.SelectedIndex == -1)
                MessageBox.Show("Select a person first!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                var selectedName = unemployedListView.SelectedItem as Person;
                var employPersonWindow = new EmployPersonWindow(selectedName);

                employPersonWindow.PersonEmployed += OnPersonEmployed;
                

                employPersonWindow.ShowDialog();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayUnemployedFromDatabase();
        }

        public void OnPersonEmployed(object source, EventArgs e)
        {
            DisplayUnemployedFromDatabase();
        }
    }
}
