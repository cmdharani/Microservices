using Mango.services.CouponAPI.Data;
using Mango.services.CouponAPI.Models;
using Mango.services.CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private  ResponseDto _response;

        public CouponAPIController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _response = new ResponseDto();
        }


        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> coupons = _dbContext.Coupons.ToList();
                _response.Result = coupons;
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Message = ex.Message;
                
            }
            return _response;    
        }

        [HttpGet]
        [Route("id:int")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon coupon = _dbContext.Coupons.First(x => x.CouponId == id);
                _response.Result = coupon;
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
