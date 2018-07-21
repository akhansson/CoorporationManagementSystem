using CooperationApp.Coorperation;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CooperationApp
{
    /// <summary>
    /// Interaction logic for DisplayCompanies.xaml
    /// </summary>
    public partial class DisplayCompanies : Window, ICompanyDatabase
    {
        // The path of the database
        static string databaseName = "Companies.db";
        static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string databasePath = System.IO.Path.Combine(folderPath, databaseName);

        public DisplayCompanies()
        {
            InitializeComponent();
            ReadCompanyDatabase();
        }

        public void ReadCompanyDatabase()
        {
            List<Company> companies;

            using (SQLiteConnection companyConnection = new SQLiteConnection(databasePath))
            {
                companyConnection.CreateTable<Company>();
                companies = companyConnection.Table<Company>().ToList();
            }

            if (companies != null)
            {
                foreach (var company in companies)
                {
                    companiesListView.Items.Add(new ListViewItem()
                    {
                        Content = company.CompanyName
                    });
                }
            }
        }
    }
}
