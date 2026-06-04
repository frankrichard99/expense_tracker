namespace ExpenseTracker.Models
{
    public class User
    {
        public required string Id { set; get; }
        public required string FirstName { set; get; }
        public required string LastName { set; get; }
        public int? Age { set; get; }

    }
}
