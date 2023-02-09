using CRM.Enums;
using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        public Currency Currency { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public AppUser User { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime? LockDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}