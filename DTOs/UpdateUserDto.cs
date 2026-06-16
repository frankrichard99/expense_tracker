namespace ExpenseTracker.DTOs
{
    public class UpdateUserDto
    {
        public string? FirstName { set; get; }
        public string? LastName { set; get; }
        public string? Email { set; get; }
        public int? Age { set; get; }
    }
}
