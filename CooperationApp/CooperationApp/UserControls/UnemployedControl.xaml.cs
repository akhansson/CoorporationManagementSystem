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
            unemployedListView.ItemsSource = unemployed.Select(c => c.FullName);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayUnemployedFromDatabase();
        }
    }
}
