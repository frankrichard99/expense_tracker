using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models

{
    public class ExpenseCategory
    {
        public int Id { get; set; }
        public required string Name { set; get; }

        public List<Expense> Expenses { get; set; } = new();
    }
    
    public class Expense
    {
        public int Id { set; get; }

        [Column(TypeName = "decimal(18,2)")]
        public required decimal Amount{ set; get; }
        public required string Description { set; get; }
        public DateTime Date { set; get; } = DateTime.UtcNow;
        public required int UserId { set; get; }
        public User User { set; get; }

        public required int CategoryId { set; get; }
        public ExpenseCategory Category { set; get; }
    }

}