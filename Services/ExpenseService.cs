using ExpenseTracker.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ExpenseTracker.Services
{
    public class ExpenseService
    {
        static private List<Expense> expenses = new List<Expense>
        {
            new Expense
            {
                Id = 101,
                Amount = 15.50,
                Description = "Lunch at McDonald's",
                Category = ExpenseCategory.FoodCategory,
                Date = DateTime.Now.AddDays(-1)
            },
            new Expense
            {
                Id = 102,
                Amount = 30.00,
                Description = "Train ticket",
                Category = ExpenseCategory.TrasportCategory,
                Date = DateTime.Now
            },

            new Expense
            {
                Id = 103,
                Amount = 85.00,
                Description = "Groceries for the week",
                Category = ExpenseCategory.FoodCategory,
                Date = DateTime.Now.AddDays(-3)
            },
            new Expense
            {
                Id = 104,
                Amount = 350.00,
                Description = "Data Science Boot Camp Deposit",
                Category = ExpenseCategory.EducationCategory,
                Date = DateTime.Now.AddDays(-5)
            }
        };
        public List<Expense> GetAllExpenses()
        {
            return expenses;
        }
        public Expense GetExpenseById(int id) { 
            var expense =  expenses.FirstOrDefault(e => e.Id == id);
            return expense;
        }
    }
}
