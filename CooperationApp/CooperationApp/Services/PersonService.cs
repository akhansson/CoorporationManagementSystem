using CooperationApp.Data;
using CooperationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            throw new NotImplementedException();
        }

        public List<Person> GetAllPeople()
        {
            return _personRepository.GetAllPeople();
        }

        public string PeopleAmount()
        {
            var peopleAmount = _personRepository.GetAllPeople().Count;
            return $"{peopleAmount}";
        }
    }
}
