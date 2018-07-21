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

            MessageBox.Show("Company sucessfully added!", "Company added", MessageBoxButton.OK, MessageBoxImage.Information);
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
                throw new ArgumentException("You didn't write a company name!", "Error: Whitespace or empty field");
            }

            // Exception thrown if other characters than letters and spaces are provided.
            if (!Regex.IsMatch(company.CompanyName, @"^[A-Za-zÅÄÖåäö ]+$"))
            {
                throw new ArgumentException("You can only write letters and spaces!");
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

        public string CompanyAmount()
        {
            var companies = GetAllCompanies();

            return $"{companies.Count} companies in the database";
        }
    }
}
