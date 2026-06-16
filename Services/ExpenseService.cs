using ExpenseTracker.Data;
using ExpenseTracker.DTOs;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Services
{
    public class ExpenseService
    {
        private readonly AppDbContext _context;
        public ExpenseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Expense>> GetAllExpenses()
        {
            return await _context.Expenses
                .AsNoTracking()
                .Include(e => e.User)
                .Include(e => e.Category)
                .ToListAsync();
        }

        public async Task<Expense?> GetExpenseById(int id)
        {
            return await _context.Expenses
                .AsNoTracking()
                .Include(e => e.User)
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Expense?> AddExpense(decimal amount, string description, int userId, string categoryName)
        {
            ExpenseCategory? category = await _context.ExpenseCategories.FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (category == null || user == null)
            {
                return null;
            }

            var newExpense = new Expense
            {
                Amount = amount,
                Description = description,
                UserId = userId,
                CategoryId = category.Id,
            };

            await _context.Expenses.AddAsync(newExpense);
            await _context.SaveChangesAsync();
            return newExpense;
        }

        public async Task<Expense?> UpdateExpense(int id, decimal? amount, string? description, string? categoryName)
        {
            var existingExpense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
            if (existingExpense == null) return null;

           

          if(amount.HasValue)
            {
                existingExpense.Amount = amount.Value;
            };
            if (description != null)
            {
                existingExpense.Description = description;
            }
            if(categoryName != null)
            {
                ExpenseCategory? category = await _context.ExpenseCategories.FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());
                if (category == null) return null;
                existingExpense.CategoryId = category.Id;
            }
            

            await _context.SaveChangesAsync();

           
            return await GetExpenseById(id);
        }

      
        public async Task<bool> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
            if (expense == null) return false;

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}