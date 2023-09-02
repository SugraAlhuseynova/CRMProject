using System.ComponentModel.DataAnnotations;

namespace CRM.DTO
{
    public class UserRegistrationDto
    {
        [MaxLength(30)]
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [MaxLength(30)]
        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Birthdate is required")]
        public DateTime BirthDate { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Required(ErrorMessage = "Password is Required")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="Address is required")]
        public string Address { get; set; }
    }
}
