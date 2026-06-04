using ExpenseTracker.Models;

namespace ExpenseTracker.DTOs
{
    public class CreateExpenseDto
    {
      
        public required double Amount { set; get; }
        public required string Description { set; get; }
        public required ExpenseCategory Category { set; get; }
        public DateTime Date { set; get; }
    }
}
