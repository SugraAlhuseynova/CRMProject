using Microsoft.Build.Framework;

namespace CRM.DTO
{
    public class ForgotPasswordDto
    {
        [Required]
        public string Email { get; set; }
    }
}
