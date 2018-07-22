using SQLite;

namespace CooperationApp.Models
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string FullName { get; set; }

        // We can't add the type Company to a SQLite Database List.
        // I think every company should have a special key that can be added to the Person class if he is employed or something similar.
        //
        // [MaxLength(50)]
        // public Company Enterprise { get; set; }
    }
}
