using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.DTOs
{
    public class UpdateExpenseDto
    {
        
        public decimal? Amount { get; set; }

        public string? Description { get; set; }
        public string? CategoryName { get; set; }
    }
}