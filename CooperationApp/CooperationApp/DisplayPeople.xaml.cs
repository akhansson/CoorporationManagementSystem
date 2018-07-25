﻿using CooperationApp.Models;
using CooperationApp.Services;
using CooperationApp.UserControls;
using Core.Util;
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
using System.Windows.Shapes;

namespace CooperationApp
{
    /// <summary>
    /// Interaction logic for DisplayPeople.xaml
    /// </summary>
    public partial class DisplayPeople : Window
    {
        private PersonService _personService;

        public Event Event = new Event();

        public DisplayPeople()
        {
            _personService = new PersonService();

            InitializeComponent();

            DisplayPeopleFromDatabase();
        }

        public void DisplayPeopleFromDatabase()
        {
            var people = _personService.GetAllPeople();
            personListView.ItemsSource = people;
        }

        private void deletePersonButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPerson = personListView.SelectedItem as Person;

            _personService.RemovePerson(selectedPerson);

            DisplayPeopleFromDatabase();
            Event.Trigger("deletedPepole");
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
