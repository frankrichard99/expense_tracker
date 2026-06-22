using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { set; get; }

        [Required]
        [MinLength(5)]
        public string Password { set; get; }
    }
}
