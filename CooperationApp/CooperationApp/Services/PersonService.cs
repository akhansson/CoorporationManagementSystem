using CooperationApp.Data;
using CooperationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        public string PeopleAmount()
        {
            var peopleAmount = _personRepository.GetAllPeople().Count;
            return $"{peopleAmount}";
        }
    }
}
