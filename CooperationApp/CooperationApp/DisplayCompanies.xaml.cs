using CooperationApp.Coorperation;
using CooperationApp.Services;
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
        private CompanyService _companyService;

        public DisplayCompanies()
        {
            InitializeComponent();
            ReadCompanyDatabase();

            _companyService = new CompanyService();

        }

        public void ReadCompanyDatabase()
        {
            var companies = _companyService.GetAllCompanies();

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
