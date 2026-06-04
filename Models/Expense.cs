namespace ExpenseTracker.Models
{
    public class ExpenseCategory
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public double Limit { set; get; }

        public static ExpenseCategory TrasportCategory { get; } = new ExpenseCategory { Id = 101, Name = "Transport", Limit = 1200.0 };
        public static ExpenseCategory EducationCategory { get; } = new ExpenseCategory { Id = 102, Name = "Education", Limit = 1000.0 };
        public static ExpenseCategory FoodCategory { get; } = new ExpenseCategory { Id = 103, Name = "Food", Limit = 2000.0 };
    }
    
    public class Expense
    {
        public required int Id { set; get; }
        public required double Amount{ set; get; }
        public required string Description { set; get; }
        public required ExpenseCategory Category { set; get; }
        public DateTime Date { set; get; }
    }
}
