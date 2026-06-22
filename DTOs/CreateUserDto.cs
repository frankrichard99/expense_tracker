
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.DTOs
{
    public class CreateUserDto
    {
       
        
            [MaxLength(50)]
            [Required]
            public string FirstName { set; get; }

            [MaxLength(50)]
        [Required]
        public string LastName { set; get; }

            [EmailAddress]
        [Required]
        public string Email { set; get; }

            [Range(12, 100)]
        [Required]
        public int Age { set; get; }

        
    }
}
