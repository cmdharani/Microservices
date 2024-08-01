using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPINew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {

        [HttpPost("Register")]
        public async Task<IActionResult> Register()
        {
            return Ok("Registered Man");
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login()
        {
            return Ok("Logged in");
        }
    }
}
