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
        private readonly IJwtTokenGenerator _jwtTokenGenerator;


        public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(x=>x.UserName.ToLower()==loginRequestDto.UserName.ToLower());
        
            if(user != null)
            {
               var isValid= await _userManager.CheckPasswordAsync(user,loginRequestDto.Password);

                if(isValid)
                {

                    var token = _jwtTokenGenerator.GenerateToken(user);

                    UserDto userDto = new UserDto { 
                    
                        Email=user.Email,
                        Id=user.Id,
                        Name=user.Name = user.Name,
                        PhoneNumber=user.PhoneNumber
                    };


                    return new LoginResponseDto {
                                Token= token,
                                User= userDto
                    };
                }

            }

            return new LoginResponseDto {User=null, Token=string.Empty };
        
        
        
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


            

        }
    }
}
