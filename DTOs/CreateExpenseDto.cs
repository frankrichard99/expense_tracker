using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.DTOs
{
    public class CreateExpenseDto
    {
        [Required]
        public decimal Amount { set; get; }

        [Required]
        [MaxLength(200)]
        public string Description { set; get; }

        [Required]
        public string CategoryName { set; get; }

        public IFormFile? ReceiptImage { get; set; }
    }
}
