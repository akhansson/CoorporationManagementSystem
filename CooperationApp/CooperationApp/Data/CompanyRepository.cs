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

        public void RemoveCompany(Company company)
        {
            using (var connection = DbSQLite.CreateConnection())
            {
                connection.BeginTransaction();
                try
                {
                    connection.CreateTable<Company>();

                    var persons = connection.Table<Person>().Where(p => p.CompanyId == company.Id).ToList();
                    foreach (var person in persons)
                    {
                        person.CompanyId = null;
                    }
                    connection.UpdateAll(persons);

                    connection.Delete(company);

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


                List<CompanyCount> query = connection.Query<CompanyCount>("SELECT Company.CompanyName, Person.CompanyId as Id, COUNT(Person.CompanyId) as NumberOfPersons  FROM Person inner JOIN Company ON person.CompanyId = Company.Id where Company.CompanyName is not null GROUP BY Company.CompanyName");
                


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
