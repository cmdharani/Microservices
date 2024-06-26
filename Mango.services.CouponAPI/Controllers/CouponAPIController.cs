﻿using AutoMapper;
using Mango.services.CouponAPI.Data;
using Mango.services.CouponAPI.Models;
using Mango.services.CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]  // it is related to BackendApiAuthenticationHttpClientHandler
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private  ResponseDto _response;
        private IMapper _mapper;

        public CouponAPIController(AppDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _response = new ResponseDto();
            _mapper = mapper;
        }


        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> coupons = _dbContext.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(coupons);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Message = ex.Message;
                
            }
            return _response;    
        }

        //[HttpGet]
        //[Route("id:int")]
        //[Route("{id:int}")]
        [HttpGet("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon coupon = _dbContext.Coupons.First(x => x.CouponId == id);
                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                //Coupon? coupon = _dbContext.Coupons.FirstOrDefault(x => x.CouponCode.ToLower() == code.ToLower());
                Coupon? coupon = _dbContext.Coupons.FirstOrDefault(x => x.CouponCode.Equals(code, StringComparison.CurrentCultureIgnoreCase));

                if (coupon == null)
                {
                    _response.isSuccess = false;
                }
                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _dbContext.Coupons.Add(obj);
                _dbContext.SaveChanges();

                _response.Result=_mapper.Map<CouponDto>(obj);
                _response.isSuccess=true;
                
               
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _dbContext.Coupons.Update(obj);
                _dbContext.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(obj);
                _response.isSuccess = true;


            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                var coupon = _dbContext.Coupons.First(x => x.CouponId == id);
                if(coupon != null)
                {
                    _dbContext.Remove(coupon);
                    _dbContext.SaveChanges();
                    _response.isSuccess = true;
                }
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
