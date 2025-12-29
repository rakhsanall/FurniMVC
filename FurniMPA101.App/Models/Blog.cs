namespace FurniMPA101.App.Models
{
    public class Blog:BaseEntity
    {
   
        public string Title { get; set; }
        public string Content { get; set; }

        public string ImageUrl { get; set; }
        public string ImageName { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
