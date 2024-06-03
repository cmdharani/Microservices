﻿using Mango.web.Models;
using Mango.web.Service.Iservice;
using Mango.web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Mango.web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService _authService)
        {
            authService = _authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }




        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto obj)
        {

            ResponseDto responseDto = await authService.LoginAsync(obj);

            if (responseDto != null && responseDto.isSuccess)
            {
                LoginResponseDto loginRequestDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));
                return  RedirectToAction("Index","Home");

            }
            else
            {
                ModelState.AddModelError("custom Error",responseDto.Message);
                return View(obj);
            }

        }


        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                new SelectListItem{Text=SD.RoleCustomer,Value=SD.RoleCustomer},
            };

            ViewBag.RoleList = roleList;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {

            var result = await authService.RegisterAsync(obj);
            ResponseDto assignRole = new ResponseDto();

            if (result != null && result.isSuccess)
            {
                if (string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = SD.RoleCustomer;
                }

                assignRole = await authService.AssignRoleAsync(obj);
                if (assignRole != null && assignRole.isSuccess)
                {
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
                }
                

            }
            else
            {
                TempData["error"] = result.Message;
            }

            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                new SelectListItem{Text=SD.RoleCustomer,Value=SD.RoleCustomer},
            };

            ViewBag.RoleList = roleList;
            return View(obj);


        }


        public IActionResult Logout()
        {
            return View();
        }

    }
}
