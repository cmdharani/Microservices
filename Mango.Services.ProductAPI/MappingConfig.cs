using AutoMapper;
using Mango.Services.ProductAPI.Model;
using Mango.Services.ProductAPI.Model.DTO;
namespace Mango.services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => {

                config.CreateMap<ProductDto,Product>();
                config.CreateMap<Product, ProductDto>();
            
            });

            return mappingConfig;
        }
    }
}
