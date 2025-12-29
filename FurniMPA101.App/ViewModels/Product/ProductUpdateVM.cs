namespace FurniMPA101.App.ViewModels.Product
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public IFormFile? Image { get; set; }
        public string ImageName { get; set; }
        public string? ImageUrl { get; set; }
    }
}
