using CRM.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
    public class AppUser:IdentityUser
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
        [Required]
        public Status Status { get; set; }
        public bool IsBanned { get; set; }
        public bool IsDeleted { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
