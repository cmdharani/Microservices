using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPINew.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; } = string.Empty;
    }
}
