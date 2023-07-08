using Mango.Services.EmailAPI.Data;
using Mango.Services.EmailAPI.Message;
using Mango.Services.EmailAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Mango.Services.EmailAPI.Services
{
    public class EmailService : IEmailService
    {
        private DbContextOptions<AppDbContext> _dpOptions;

        public EmailService(DbContextOptions<AppDbContext> dbOptions)
        {
            _dpOptions = dbOptions;
        }

        public async Task EmailCartAndLog(CartDto cartDto)
        {
            StringBuilder message = new StringBuilder();

            message.AppendLine("<br/>Cart Email Requested");
            message.AppendLine("<br/>Total " + cartDto.CartHeader.CartTotal);
            message.Append("<br/>");
            message.Append("<ul>");

            foreach(var item in cartDto.CartDetails)
            {
                message.Append("<li>");
                message.Append(item.Product.Name + " x " + item.Count);
                message.Append("<li/>");
            }
            message.Append("<ul/>");

            await LogAndEmail(message.ToString(), cartDto.CartHeader.Email);
        }

        public async Task EmailUserRegisterAndLog(string email)
        {
            StringBuilder message = new StringBuilder();

            message.AppendLine("<br/>New User Registered with the email: " + email);

            await LogAndEmail(message.ToString(), email);
        }

        public async Task LogOrderPlaced(RewardsMessage rewardsDto)
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine("New Order Placed. <br/> Order ID " + rewardsDto.OrderId);
            await LogAndEmail(message.ToString(), "admin@octocat.com");
        }

        private async Task<bool> LogAndEmail(string message, string email)
        {
            try
            {
                EmailLogger emailLog = new()
                {
                    Email = email,
                    EmailSent = DateTime.Now,
                    Message = message
                };

                await using var _db = new AppDbContext(_dpOptions);
                await _db.EmailLoggers.AddAsync(emailLog);
                await _db.SaveChangesAsync();

                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
    }
}
