using AutoMapper;
using Mango.services.CouponAPI.Models;
using Mango.services.CouponAPI.Models.DTO;
namespace Mango.services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => {

                config.CreateMap<CouponDto,Coupon>();
                config.CreateMap<Coupon, CouponDto>();
            
            });

            return mappingConfig;
        }
    }
}
