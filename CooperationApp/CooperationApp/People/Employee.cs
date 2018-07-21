using CooperationApp.Coorperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CooperationApp.People
{
    class Employee : Person
    {
        public Company CompanyName { get; set; }

        public Employee(string fullName, Company companyName)
            :base(fullName)
        {
            CompanyName = companyName;
        }
    }
}
