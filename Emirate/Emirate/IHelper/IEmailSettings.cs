namespace Emirate.IHelper
{
    public interface IEmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public string AdminEmail { get; set; }
    }
}
