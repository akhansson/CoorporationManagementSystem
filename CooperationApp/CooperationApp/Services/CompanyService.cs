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
    public class CompanyService
    {
        private CompanyRepository _companyRepository;

        public CompanyService()
        {
            _companyRepository = new CompanyRepository();
        }

        public void AddCompany(Company company)
        {
            ValidateCompany(company);

            _companyRepository.AddCompany(company);

        }

        public void AddPerson(Person person)
        {
            ValidatePerson(person);

            _companyRepository.AddPerson(person);
        }

        
        private static void ValidateCompany(Company company)
        {
            // Outputs an error if the company name provided is null or an empty string or empty characters
            if (string.IsNullOrWhiteSpace(company.CompanyName))
            {
                throw new ArgumentException("You didn't write anything in the field!", "Error: No company name written");
            }

            // This code runs if the company name provided is composed of letters. No other characters are accepted.
            if (!Regex.IsMatch(company.CompanyName, @"^[A-Za-zÅÄÖåäö ]+$"))
            {
                throw new ArgumentException("You should only provide letters in the company name. No other characters are supported!");
            }
        }

        private void ValidatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public List<Company> GetAllCompanies()
        {
            return _companyRepository.GetAllCompanies();
        }

        public List<Person> GetAllPeople()
        {
            return _companyRepository.GetAllPeople();
        }
    }
}
