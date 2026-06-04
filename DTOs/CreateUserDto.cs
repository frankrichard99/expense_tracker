namespace ExpenseTracker.DTOs
{
    public class CreateUserDto
    {
       
        
           
            public required string FirstName { set; get; }
            public required string LastName { set; get; }
            public required string Email { set; get; }
            public required int Age { set; get; }

        
    }
}
