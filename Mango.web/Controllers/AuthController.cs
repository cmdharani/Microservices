using Mango.web.Models;
using Mango.web.Service;
using Mango.web.Service.Iservice;
using Mango.web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mango.web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        private readonly ITokenProvider tokenProvider;


        public AuthController(IAuthService _authService, ITokenProvider _tokenProvider)
        {
            authService = _authService;
          tokenProvider = _tokenProvider;
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
                LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));

                tokenProvider.SetToken(loginResponseDto.Token);

                await SignInUser(loginResponseDto);
                return  RedirectToAction("Index","Home");

            }
            else
            {
                TempData["error"] = responseDto.Message;
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


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }


        private async Task SignInUser(LoginResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(model.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));


            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role,
                jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));



            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }


    }
}
