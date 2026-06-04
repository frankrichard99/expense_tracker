namespace ExpenseTracker.Models
{
    public class User
    {
        public required int Id { set; get; }
        public required string FirstName { set; get; }
        public required string LastName { set; get; }
        public required string Email { set; get; }
        public required int Age { set; get; }

        public static int UserCount = 0;

        public static int NextId()
        {
            return ++UserCount;
        }
    }
}
