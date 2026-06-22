using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.DTOs
{
    public class UpdateExpenseDto
    {
        [Range(0.01, 10000000)]
        public decimal? Amount { get; set; }

        
        [MaxLength(200)]
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
    }
}