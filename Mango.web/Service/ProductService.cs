using Mango.web.Models;
using Mango.web.Service.Iservice;
using Mango.web.Utility;

namespace Mango.web.Service
{
    public class ProductService:IProductService
    {
        private readonly IBaseService _baseService;

        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateProductAsync(ProductDto productDto)
        {
            return await _baseService.sendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data=productDto,
                Url = SD.ProductAPIBase + "/api/product"

            });
        }

        public async Task<ResponseDto?> DeleteProductAsync(int id)
        {
            return await _baseService.sendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "/api/product/" + id

            });
        }

        public async Task<ResponseDto?> GetAllProductAsync()
        {
            return await _baseService.sendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product"

            });

        }

        public async Task<ResponseDto?> GetProductAsync(string productId)
        {
            return await _baseService.sendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/productId/GetByCode/" + productId

            });
        }

        public async Task<ResponseDto?> GetProductByIdAsync(int id)
        {
            return await _baseService.sendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product/" + id

            });
        }

        public async Task<ResponseDto?> UpdateProductAsync(ProductDto productDto)
        {
            return await _baseService.sendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/product"

            });
        }
    }
}
