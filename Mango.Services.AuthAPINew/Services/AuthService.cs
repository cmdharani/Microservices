using Mango.services.AuthAPINew.Data;
using Mango.Services.AuthAPINew.DTO;
using Mango.Services.AuthAPINew.Models;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPINew.Services
{
    public class AuthService : IAuthService
    {

        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = registrationRequestDto.Email,
                UserName = registrationRequestDto.Email,
                PhoneNumber = registrationRequestDto.PhoneNumber,
                Name = registrationRequestDto.Name,
                NormalizedEmail = registrationRequestDto.Email.ToUpper()

            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(x => x.Email.ToLower() == registrationRequestDto.Email.ToLower());

                    UserDto newUser = new()
                    {
                        Email = userToReturn.Email,
                        Name = userToReturn.Name,
                        Id = userToReturn.Id,
                        PhoneNumber = userToReturn.PhoneNumber
                    };

                    return string.Empty;

                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                    
                }
            }
            catch (Exception ex)
            {

                return "Error Occured";
            }


            return "Error Occured";

        }
    }
}
