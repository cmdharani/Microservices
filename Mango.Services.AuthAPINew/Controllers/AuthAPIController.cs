using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Mango.Services.AuthAPINew.Services;
using Mango.Services.AuthAPINew.DTO;

namespace Mango.Services.AuthAPINew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ResponseDto _response;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            string errorMessage =await _authService.Register(registrationRequestDto);
            if(!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(errorMessage);
            }
            return Ok(_response);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto loginRequestDto)
        {
            var loginResult=await _authService.Login(loginRequestDto);

            if(loginResult.User != null ) //&& loginResult.Token != string.Empty)
            {
                _response.Message = "Logged in Successful";
                return Ok(_response);
            }
            else
            {
                _response.IsSuccess = false;
                _response.Message = "username and Password is Incorrect";
                return BadRequest(loginResult);
            }
        }
    }
}
