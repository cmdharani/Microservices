
namespace Mango.Service.EmailAPI.Model.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }=string.Empty;
        public double Price { get; set; }
        public string Description { get; set; }=string.Empty;
        public string CategoryName { get; set; }= string.Empty;
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
        public int Count { get; set; } = 1;
    }
}
