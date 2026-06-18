
namespace ExpenseTracker.DTOs
{
    public class CreateExpenseDto
    {
      
        public required decimal Amount { set; get; }
        public required string Description { set; get; }
        public required string CategoryName { set; get; }
    }
}
