namespace FurniMPA101.App.Models
{
    public class Product:BaseEntity
    {

        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
        public bool IsDeleted { get; set; }

    }
}
