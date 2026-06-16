namespace ExpenseTracker.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { set; get; }
        public required string LastName { set; get; }
        public required string Email { set; get; }
        public required string PasswordHash { set; get; }
        public string Role { get; set; } = "User";
        public required int Age { set; get; }
        public List<Expense> Expenses { set; get; } = new();

        
    }
}
