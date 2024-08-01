using Mango.Services.AuthAPINew.DTO;

namespace Mango.Services.AuthAPINew.Services
{
    public interface IAuthService
    {

        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
