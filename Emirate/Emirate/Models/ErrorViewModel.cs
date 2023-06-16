using Microsoft.AspNetCore.Identity;

namespace Emirate.Models
{
    public class ErrorViewModel: IdentityUser
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}