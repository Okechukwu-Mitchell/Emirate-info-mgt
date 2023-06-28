using Emirate.Models;
using Microsoft.Extensions.Options;

namespace Emirate.IHelper
{
    public interface IEmailHelper
    {
        public bool SendAdminEmail(ApplicationUser user);
        //EmailService(IOptions<EmailSettings> emailSettings);
        //void SendEmail(string to, string subject, string body);
    }
}
