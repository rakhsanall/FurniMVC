namespace FurniMPA101.App.Models
{
    public class Comment:BaseEntity
    {
     
        public string Description { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public bool IsAccepted { get; set; }
    }
}
