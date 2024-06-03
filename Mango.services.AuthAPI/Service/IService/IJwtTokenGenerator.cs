using Mango.services.AuthAPI.Migrations;
using Mango.Services.AuthAPI.Models;

namespace Mango.services.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
