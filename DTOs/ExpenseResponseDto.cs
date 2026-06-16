namespace ExpenseTracker.DTOs
{
    public class ExpenseResponseDto
    {
        public int Id { set; get; }
        public decimal Amount { set; get; }
        public string Description { set; get; }
        public DateTime Date { set; get; }

        public int UserId { set; get; }
        public UserPreviewDto User { set; get; }

        public int CategoryId { set; get; }
        public CategoryPreviewDto Category { set; get; }
    }

    public class UserPreviewDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class CategoryPreviewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    
    }
}