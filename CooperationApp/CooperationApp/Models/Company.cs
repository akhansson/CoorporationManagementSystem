using SQLite;

namespace CooperationApp.Models
{
    public class Company
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string CompanyName { get; set; }

        //public override string ToString()
        //{
        //    return CompanyName;
        //}
    }
}
