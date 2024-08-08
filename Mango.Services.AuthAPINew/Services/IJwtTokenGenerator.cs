using Mango.Services.AuthAPINew.Models;

namespace Mango.Services.AuthAPINew.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
