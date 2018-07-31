namespace CooperationApp.Models
{
    public class CompanyPerson
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }

    }

}
