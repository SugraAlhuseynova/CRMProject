using System.ComponentModel.DataAnnotations;

namespace CRM.DTO
{
    public class ForgotPasswordDto
    {
        [Required]
        public string Email { get; set; }
    }
}
