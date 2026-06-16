using System;
using System.Collections.Generic;

namespace ExpenseTracker.DTOs
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required int Age { get; set; }

      
        public List<ExpensePreviewDto> Expenses { get; set; } = new();
    }

    public class ExpensePreviewDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}