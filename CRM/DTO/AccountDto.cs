using CRM.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.DTO
{
    public class AccountDto
    {
       
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Required]
        public Currency Currency { get; set; }

    }
}
