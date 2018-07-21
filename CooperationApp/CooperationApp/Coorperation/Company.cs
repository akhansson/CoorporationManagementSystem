using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CooperationApp.Coorperation
{
    public class Company
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string CompanyName { get; set; }
    }
}
