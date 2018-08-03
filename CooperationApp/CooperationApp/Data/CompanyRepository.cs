using System;
using System.Collections.Generic;
using System.Linq;
using CooperationApp.Models;
using SQLite;

namespace CooperationApp.Data
{
    public interface ICompanyRepository
    {
    }

    public class CompanyRepository : ICompanyRepository
    {
        private PersonRepository _personRepository;

        public CompanyRepository()
        {
            _personRepository = new PersonRepository();
        }

        public void AddCompany(Company company)
        {
            // Initialize the SQLite connection
            // The "using" statement takes care of closing the connection"
            using (var connection = DbSQLite.CreateConnection())
            {
                // Create a table of the type Person
                connection.CreateTable<Company>();
                // Insert the person object that was created when the save button was clicked into the SQLite table.
                connection.Insert(company);
            }
        }

        public void RemoveCompany(List<Company> companies)
        {
            using (var connection = DbSQLite.CreateConnection())
            {
                connection.BeginTransaction();
                try
                {
                    connection.CreateTable<Company>();

                    var companiesToBeDeleted = companies;


                    foreach (var company in companiesToBeDeleted)
                    {
                        var companyToBeDeletedList = connection.Table<Company>().Where(c => c.CompanyName == company.CompanyName).ToList();

                        // Unemploy all people in the company
                        foreach (var c in companyToBeDeletedList)
                        {
                            var employedPeopleList = _personRepository.GetEmployees(c);

                            if (employedPeopleList.Count != 0)
                            {
                                foreach (var person in employedPeopleList)
                                {

                                    connection.CreateTable<Person>();

                                    var selectedPerson = connection.Table<Person>().Where(p => p.Id == person.Id).ToList();

                                    foreach (var p in selectedPerson)
                                    {
                                        p.CompanyId = null;
                                    }

                                    connection.UpdateAll(selectedPerson);

                                }
                            }
                            connection.Delete(c);
                        }
                    }

                    connection.Commit();
                }
                catch (Exception)
                {
                    connection.Rollback();
                    throw;
                }
            }
        }


        public List<Company> GetAllCompanies()
        {
            using (var companyConnection = DbSQLite.CreateConnection())
            {
                companyConnection.CreateTable<Company>();
                return companyConnection.Table<Company>().ToList();
            }
        }

        public List<CompanyCount> AmountOfEmployees()
        {
            using (var connection = DbSQLite.CreateConnection())
            {
                connection.CreateTable<Company>();
                connection.CreateTable<Person>();


                List<CompanyCount> query = connection.Query<CompanyCount>("SELECT Company.CompanyName, Person.CompanyId as Id, COUNT(Person.CompanyId) as NumberOfPersons FROM Company left JOIN Person ON person.CompanyId = Company.Id where Company.CompanyName is not null GROUP BY Company.CompanyName order by Company.CompanyName");



                return query;
            }
        }

        public bool CompanyExists(Company company)
        {
            using (var connection = DbSQLite.CreateConnection())
            {
                connection.CreateTable<Company>();

                var comp = connection.Table<Company>().SingleOrDefault(c => c.CompanyName.ToLowerInvariant() == company.CompanyName.ToLowerInvariant());

                if (comp == null)
                {
                    return false;
                }
                else
                    return true;
            }
        }


        public List<Company> SearchCompany(string searchString)
        {
            using (var connection = DbSQLite.CreateConnection())
            {
                connection.CreateTable<Company>();

                return connection.Table<Company>()
                       .Where(c => c.CompanyName.ToUpper().Contains(searchString.ToUpper()))
                       .ToList();
            }
        }
    }
}
