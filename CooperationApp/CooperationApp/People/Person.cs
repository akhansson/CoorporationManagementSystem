using CooperationApp.Coorperation;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CooperationApp.People
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string FullName { get; set; }
        [MaxLength(50)]
        public Company Company { get; set; }
    }
}
