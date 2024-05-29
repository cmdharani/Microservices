﻿using Mango.web.Models;
using Mango.web.Service.Iservice;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }



        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto> list = new List<CouponDto>();

            ResponseDto? response = await _couponService.GetAllCouponAsync();

            if (response != null && response.isSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }

            return View(list);
        }
    }
}
