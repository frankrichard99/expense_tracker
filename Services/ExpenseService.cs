using ExpenseTracker.Data;
using ExpenseTracker.DTOs;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Services
{
    public class ExpenseService
    {
        private readonly AppDbContext _context;
        private readonly CloudinaryService _cloudinaryService;
        public ExpenseService(AppDbContext context, CloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
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

        public async Task<List<Expense>> GetExpensesByUserId(int userId)
        {
            return await _context.Expenses
                .Where(e => e.UserId == userId)
                .Include(e => e.Category)
                .ToListAsync();
        }

        public async Task<Expense?> AddExpense(decimal amount, string description, int userId, string categoryName, IFormFile? receiptImage)
        {
            ExpenseCategory? category = await _context.ExpenseCategories.FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (category == null || user == null)
            {
                return null;
            }
            if(receiptImage == null)
            {
                return null;
            }
            string? receiptUrl = null;
            if (receiptImage != null && receiptImage.Length > 0)
            {
                string newReceiptUrl = await _cloudinaryService.UploadFile(receiptImage);
                receiptUrl = newReceiptUrl;
            }
            var newExpense = new Expense
            {
                Amount = amount,
                Description = description,
                UserId = userId,
                CategoryId = category.Id,
                ReceiptUrl = receiptUrl,
            };

            await _context.Expenses.AddAsync(newExpense);
            await _context.SaveChangesAsync();
            return newExpense;
        }

        public async Task<Expense?> UpdateExpense(int id, decimal? amount, string? description, string? categoryName, IFormFile? receiptImage)
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
            if (receiptImage == null)
            {
                return null;
            }
            string? receiptUrl = null;
            if (receiptImage != null && receiptImage.Length > 0)
            {
                string newReceiptUrl = await _cloudinaryService.UploadFile(receiptImage);
                receiptUrl = newReceiptUrl;
            }
            existingExpense.ReceiptUrl = receiptUrl;

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