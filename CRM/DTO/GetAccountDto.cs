using CRM.Models;

namespace CRM.DTO
{
    public class GetAccountDto
    {
        public string Name { get; set; }
        public string Currency { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime? LockDate { get; set; }
        public string Fullname { get; set; }
    }
}
