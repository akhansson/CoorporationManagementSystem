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
        public delegate void OnPersonDeletedEventHandler(object source, EventArgs e);
        public event OnPersonDeletedEventHandler PersonDeleted;

        private PersonService _personService;

        public DisplayPeople()
        {
            _personService = new PersonService();

            InitializeComponent();
        }


        public void DisplayPeopleFromDatabase()
        {
            var people = _personService.GetCompanyPersons();
            personListView.ItemsSource = people;
        }

        private void deletePersonButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCompanyPerson = personListView.SelectedItem as CompanyPerson;

            Person selectedPerson = new Person()
            {
                Id = selectedCompanyPerson.Id,
                FullName = selectedCompanyPerson.FullName,
                CompanyId = selectedCompanyPerson.CompanyId
            };

            _personService.RemovePerson(selectedPerson);

            DisplayPeopleFromDatabase();

            OnPersonDeleted();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayPeopleFromDatabase();

        }

        public virtual void OnPersonDeleted()
        {
            if (PersonDeleted != null)
            {
                PersonDeleted(this, EventArgs.Empty);
            }
        }
    }
}
