using CooperationApp.Data;
using CooperationApp.Models;
using Core.Util;
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

        public Event Event = new Event();

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

        public void RemoveCompany(Company company)
        {
            _companyRepository.RemoveCompany(company);
        }

        private async Task<int> ValidateCompany(Company company)
        {
            // Outputs an error if the company name provided is null or an empty string or empty characters
            if (string.IsNullOrWhiteSpace(company.CompanyName))
            {
                Event.Trigger("error");
                throw new ArgumentException("You didn't write a company name!", "Error: Whitespace or empty field");
            }
            // Exception thrown if other characters than letters and spaces are provided.
            if (!Regex.IsMatch(company.CompanyName, @"^[A-Za-zÅÄÖåäö ]+$"))
            {
                Event.Trigger("error");
                throw new ArgumentException("You can only write letters and spaces!", "Error: Unsupported characters provided");
            }

            // Check the API answer and return the corresponding exception or 1 when succeeded
            var companyHttp = new CooperationApp.Data.CompanyHttp();
            var result = await companyHttp.CheckIfCompanyExist(company.CompanyName);
            
            if (result == 0)
            {
                Event.Trigger("error");
                throw new ArgumentException("You didn't write a correct company name!", "Error: Company name doesn't exist");
            }
            else if (result == -1)
            {
                Event.Trigger("error");
                throw new ArgumentException("We could't check if the company exist!", "Error: Server doesn't respond");
            }
            
            return 1;
        }
        
        public List<Company> GetAllCompanies()
        {
            return _companyRepository.GetAllCompanies().OrderBy(c => c.CompanyName).ToList();
        }
        
        public List<Company> SearchCompany(string searchString)
        {
            return _companyRepository.SearchCompany(searchString);
        }

        public string CompanyAmount()
        {
            var companyAmount = GetAllCompanies().Count;

            if (companyAmount == 1)
                return $"{companyAmount} company in the database";
            else
                return $"{companyAmount} companies in the database";
        }

        
    }
}
