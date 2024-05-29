using Mango.web.Models;

namespace Mango.web.Service.Iservice
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetCouponAsync(string couponcode);
        Task<ResponseDto?> GetAllCouponAsync();
        Task<ResponseDto?> GetCouponByIdAsync(int id);
        Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto);
        Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto);
        Task<ResponseDto?> DeleteCouponAsync(int id);


    }
}
