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

        public async Task<int> AddCompany(Company company)
        {
            await ValidateCompany(company);

            _companyRepository.AddCompany(company);

            MessageBox.Show("Company sucessfully added!", "Company added", MessageBoxButton.OK, MessageBoxImage.Information);

            return 1;
        }

        public void AddPerson(Person person)
        {
             ValidatePerson(person);

            _companyRepository.AddPerson(person);
        }

        
        private static async Task<int> ValidateCompany(Company company)
        {
            var companyHttp = new CooperationApp.Data.CompanyHttp();
            var result = await companyHttp.CheckIfCompanyExist(company.CompanyName);
            
            if(result == 0)
            {
                throw new ArgumentException("You didn't write a correct company name!", "Error: Company name dosen't exiset");
            }
            else if (result == -1)
            {
                throw new ArgumentException("We could't check if the company exist!", "Error: Server dosen't respond");
            }
            // Outputs an error if the company name provided is null or an empty string or empty characters
            else if (string.IsNullOrWhiteSpace(company.CompanyName))
            {
                throw new ArgumentException("You didn't write a company name!", "Error: Whitespace or empty field");
            }

            // Exception thrown if other characters than letters and spaces are provided.
            else if (!Regex.IsMatch(company.CompanyName, @"^[A-Za-zÅÄÖåäö ]+$"))
            {
                throw new ArgumentException("You can only write letters and spaces!");
            }

            return 1;
        }

        private void ValidatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public List<Company> GetAllCompanies()
        {
            return _companyRepository.GetAllCompanies().OrderBy(c => c.CompanyName).ToList();
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
