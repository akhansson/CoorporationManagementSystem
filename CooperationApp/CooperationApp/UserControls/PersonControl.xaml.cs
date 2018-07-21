using CooperationApp.People;
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
        public PersonControl()
        {
            InitializeComponent();
        }

        private void savePersonButton_Click(object sender, RoutedEventArgs e)
        {
            // Outputting an error message if no name was provided or white spaces
            if (string.IsNullOrWhiteSpace(nameTexbox.Text))
            {
                MessageBox.Show("Could not add the person to the database. You have to provide a name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (Regex.IsMatch(nameTexbox.Text, @"^[a-zA-Z]+$"))
            {
                // If the company checkbox is unchecked
                if (isEmployedCheckbox.IsChecked == false)
                {
                    // Save Person to the database with the company set to null
                    Person person = new Person()
                    {
                        FullName = nameTexbox.Text,
                        Company = null
                    };

                    // The path of the database
                    string databaseName = "Persons.db";
                    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string databasePath = System.IO.Path.Combine(folderPath, databaseName);

                    // Initialize the SQLite connection
                    // The "using" statement takes care of closing the connection"
                    using (SQLiteConnection connection = new SQLiteConnection(databasePath))
                    {
                        // Create a table of the type Person
                        connection.CreateTable<Person>();
                        // Insert the person object that was created when the save button was clicked into the SQLite table.
                        connection.Insert(person);
                    }
                }
                // If the company checkbox is checked
                else
                {
                    // Outputting an error message if no name was provided or white spaces and if the company wasn't selected
                    if (string.IsNullOrWhiteSpace(nameTexbox.Text) && !companyCombobox.HasItems)
                    {
                        MessageBox.Show("Could not add the person to the database. You have to provide a name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    // If the name is written and the company is selected
                    MessageBox.Show("At least the company was selected. Good for you. ;)");
                }

            }
            else
            {
                MessageBox.Show("You should only provide letters in the name. No other characters are supported!", "Non letters were provided", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}
