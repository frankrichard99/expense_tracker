using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { set; get; }
        [Required]
        [MaxLength(50)]
        public string LastName { set; get; }
        [Required]
        [EmailAddress]
        public string Email { set; get; }
        
        [Required]
        [MinLength(5)]
        public string Password { set; get; }
        [Required]
        [Range(12, 100)]
        public int Age { set; get; }
        
    }
}
