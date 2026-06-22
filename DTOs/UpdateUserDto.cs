using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.DTOs
{
    public class UpdateUserDto
    {
        [MaxLength(50)]
        public string? FirstName { set; get; }
        [MaxLength(50)]
        public string? LastName { set; get; }
        [EmailAddress]
        public string? Email { set; get; }
        [Range(12, 100)]
        public int? Age { set; get; }
    }
}
