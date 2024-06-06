using Mango.web.Models;

namespace Mango.web.Service.Iservice
{
    public interface IProductService
    {
        Task<ResponseDto?> GetProductAsync(string ProductId);
        Task<ResponseDto?> GetAllProductAsync();
        Task<ResponseDto?> GetProductByIdAsync(int id);
        Task<ResponseDto?> CreateProductAsync(ProductDto productDto);
        Task<ResponseDto?> UpdateProductAsync(ProductDto productDto);
        Task<ResponseDto?> DeleteProductAsync(int id);

    }
}
