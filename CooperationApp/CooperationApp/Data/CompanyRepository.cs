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
        // The path of the database
        const string DATABASE_NAME = "Coorporation.db";
        private readonly string _databasePath;

        public CompanyRepository()
        {
            //var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var folderPath = "";
            _databasePath = System.IO.Path.Combine(folderPath, DATABASE_NAME);
        }


        public void AddCompany(Company company)
        {
            // Initialize the SQLite connection
            // The "using" statement takes care of closing the connection"
            using (var connection = CreateConnection())
            {
                // Create a table of the type Person
                connection.CreateTable<Company>();
                // Insert the person object that was created when the save button was clicked into the SQLite table.
                connection.Insert(company);
            }
        }

        public void RemoveCompany(Company company)
        {
            using (var connection = CreateConnection())
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
            using (var companyConnection = CreateConnection())
            {
                companyConnection.CreateTable<Company>();
                return companyConnection.Table<Company>().ToList();
            }
        }
        
        private SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection(_databasePath);
        }

        public List<Company> SearchCompany(string searchString)
        {
            using (var companyConnection = CreateConnection())
            {
                companyConnection.CreateTable<Company>();
                return companyConnection.Table<Company>()
                    .Where(c => c.CompanyName.Contains(searchString))
                    .ToList();
            }
        }
    }
}
