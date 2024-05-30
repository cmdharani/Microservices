using Mango.web.Models;
using Mango.web.Service.Iservice;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

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


        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponAsync(model);

                if (response != null && response.isSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }

            }
            return View(model);
        }


        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }


    }
}
