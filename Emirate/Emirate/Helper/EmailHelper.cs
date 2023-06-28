using Emirate.IHelper;
using Emirate.Models;
using Emirate.Services;
using NETCore.MailKit.Core;

namespace Emirate.Helper
{
    public class EmailHelper : IEmailHelper
    {
        private readonly IEmailSettings _emailSettings;
        private readonly IEmailServices _emailServices;
        public EmailHelper(IEmailSettings emailSettings, IEmailServices emailServices)
        {
           _emailSettings = emailSettings;
           _emailServices = emailServices;
        }

        public bool SendAdminEmail(ApplicationUser user)
        {
            if (user != null)
            {
                string toEmail = user.Email;
                string subject = "Message From Emirate University.";

                var message = "Hello" +" "+  user.FirstName + "," + " your registration on our website was successful." +
                    "</br> <br/> " + "Feel free to reach us, if u need any assistance " +
                    "</br> <br/> " + "Kind Regards," + "</br> <br/> " +


                    "Emirate University Group.";
                var isSent = _emailServices.SendEmail(toEmail, subject, message);
                if (isSent)
                {
                    return true;
                }
            }
            return false;
        }

        //public void SendEmail(string to, string subject, string body)
        //{
        //    using (var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port))
        //    {
        //        client.UseDefaultCredentials = false;
        //        client.Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password);
        //        client.EnableSsl = true;

        //        var message = new MailMessage(_emailSettings.UserName, to, subject, body);
        //        message.IsBodyHtml = true;

        //        client.Send(message);
        //    }
        //}
    }
}
