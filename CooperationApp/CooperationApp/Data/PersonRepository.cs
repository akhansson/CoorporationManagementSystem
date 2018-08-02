using System;
using System.Collections.Generic;
using System.Linq;
using CooperationApp.Models;
using SQLite;

namespace CooperationApp.Data
{
    public interface IPersonRepository
    {

    }

    public class PersonRepository : IPersonRepository
    {
        // The path of the database
        const string DATABASE_NAME = "Coorporation.db";
        private readonly string _databasePath;

        public PersonRepository()
        {
            //var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var folderPath = "";
            _databasePath = System.IO.Path.Combine(folderPath, DATABASE_NAME);
        }

        public void AddPerson(Person person)
        {
            // Initialize the SQLite connection
            // The "using" statement takes care of closing the connection"
            using (var connection = CreateConnection())
            {
                // Create a table of the type Person
                connection.CreateTable<Person>();
                // Insert the person object that was created when the save button was clicked into the SQLite table.
                connection.Insert(person);
            }
        }

        public void RemovePerson(Person person)
        {
            using (var connection = CreateConnection())
            {
                connection.CreateTable<Person>();
                connection.Delete(person);
            }
        }

        public List<Person> GetAllPeople()
        {
            using (var personConnection = CreateConnection())
            {
                personConnection.CreateTable<Person>();
                return personConnection.Table<Person>().ToList();
            }
        }

        public List<CompanyPerson> GetCompanyPersons()
        {
            using (var connection = CreateConnection())
            {
                connection.CreateTable<Company>();
                connection.CreateTable<Person>();

                var query =
                    from person in connection.Table<Person>()
                    join company in connection.Table<Company>() on person.CompanyId equals company.Id into companies
                    from company in companies.DefaultIfEmpty()
                    select new CompanyPerson
                    {
                        Id = person.Id,
                        FullName = person.FullName,
                        CompanyId = company?.Id,
                        CompanyName = company?.CompanyName
                    };

                return query.OrderBy(c => c.CompanyName).ThenBy(p => p.FullName).ToList();
            }
        }

        public void EmployPerson(int id, int companyId)
        {
            using (var connection = CreateConnection())
            {
                connection.BeginTransaction();
                try
                {
                    connection.CreateTable<Person>();

                    var selectedPerson = connection.Table<Person>().Where(p => p.Id == id).ToList();

                    foreach (var p in selectedPerson)
                    {
                        p.CompanyId = companyId;
                    }

                    connection.UpdateAll(selectedPerson);

                    connection.Commit();
                }
                catch (Exception)
                {
                    connection.Rollback();
                    throw;
                }
            }
        }

        public List<Person> GetAllUnemployed()
        {
            using (var personConnection = CreateConnection())
            {
                personConnection.CreateTable<Person>();
                return personConnection.Table<Person>().Where(c => c.CompanyId == null).ToList();
            }
        }

        public List<Person> GetEmployees(Company company)
        {
            using (var personConnection = CreateConnection())
            {
                personConnection.CreateTable<Person>();
                return personConnection.Table<Person>().Where(c => c.CompanyId == company.Id).ToList();
            }
        }

        public List<CompanyPerson> SearchCompanyPerson(string searchString)
        {
            using (var connection = CreateConnection())
            {
                connection.CreateTable<Company>();
                connection.CreateTable<Person>();

                var query =
                    from person in connection.Table<Person>()
                    join company in connection.Table<Company>() on person.CompanyId equals company.Id into companies
                    from company in companies.DefaultIfEmpty()
                    select new CompanyPerson
                    {
                        Id = person.Id,
                        FullName = person.FullName,
                        CompanyId = company?.Id,
                        CompanyName = company?.CompanyName
                    };

                return query.Where(p => p.FullName.ToLower().Contains(searchString.ToLower())).ToList();
            }
        }

        private SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection(_databasePath);
        }
    }
}
