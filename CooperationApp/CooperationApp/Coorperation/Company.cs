using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CooperationApp.Coorperation
{
    public class Company
    {
        public string CompanyName { get; private set; }

        public Company(string companyName)
        {
            CompanyName = companyName;
        }
    }
}
