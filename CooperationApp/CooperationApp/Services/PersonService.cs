using CooperationApp.Data;
using CooperationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CooperationApp.Services
{
    public class PersonService
    {
        private PersonRepository _personRepository;

        public PersonService()
        {
            _personRepository = new PersonRepository();
        }

        public void AddPerson(Person person)
        {
            ValidatePerson(person);

            _personRepository.AddPerson(person);

            MessageBox.Show($"\"{person.FullName}\" sucessfully added!", "Person added", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void RemovePerson(Person person)
        {
            _personRepository.RemovePerson(person);
        }

        public void EmployPerson(int id, int companyId)
        {
            _personRepository.EmployPerson(id, companyId);
        }

        private void ValidatePerson(Person person)
        {
            // Outputting an error message if no name was provided or white spaces
            if (string.IsNullOrWhiteSpace(person.FullName))
            {
                throw new ArgumentException("You didn't provide a name.", "Error: Name wasn't provided");
            }
            if (!Regex.IsMatch(person.FullName, @"^[A-Za-zÅÄÖåäö ]+$"))
            {
                throw new ArgumentException("You can only write letters and spaces!", "Error: Unsupported characters provided");
            }
        }

        public List<Person> GetAllPeople()
        {
            return _personRepository.GetAllPeople().OrderBy(c => c.FullName).ToList();
        }

        public List<CompanyPerson> GetCompanyPersons()
        {
            return _personRepository.GetCompanyPersons();
        }


        public List<Person> GetAllUnemployed()
        {
            return _personRepository.GetAllUnemployed().OrderBy(c => c.FullName).ToList();
        }

        public List<Person> GetEmployees(Company company)
        {
            return _personRepository.GetEmployees(company).OrderBy(c => c.FullName).ToList();
        }

        public List<CompanyPerson> SearchCompanyPerson(string searchString)
        {
            return _personRepository.SearchCompanyPerson(searchString);
        }

        public string PeopleAmount()
        {
            var peopleAmount = _personRepository.GetAllPeople().Count;

            if (peopleAmount == 1)
                return $"{peopleAmount} person in the database.";
            else
                return $"{peopleAmount} people in the database";
        }
    }
}
