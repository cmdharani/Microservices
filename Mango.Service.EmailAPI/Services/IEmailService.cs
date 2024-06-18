using Mango.Service.EmailAPI.Model.Dto;


namespace Mango.Services.EmailAPI.Services
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDto cartDto);
        Task RegisterUserEmailAndLog(string email);
        //Task LogOrderPlaced(RewardsMessage rewardsDto);
    }
}