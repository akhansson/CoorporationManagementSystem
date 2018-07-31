using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CooperationApp.Models
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string FullName { get; set; }

        [ForeignKey(typeof(Company))]
        public int? CompanyId { get; set; }
    }

}
