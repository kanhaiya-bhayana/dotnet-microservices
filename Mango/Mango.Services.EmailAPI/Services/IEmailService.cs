using Mango.Services.EmailAPI.Message;
using Mango.Services.EmailAPI.Models;

namespace Mango.Services.EmailAPI.Services
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDto cartDto);
        Task EmailUserRegisterAndLog(string email);
        Task LogOrderPlaced(RewardsMessage rewardsDto);
    }
}
