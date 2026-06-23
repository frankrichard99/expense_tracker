using ExpenseTracker.Data;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace ExpenseTracker.Services
{
    public class ReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context) => _context = context;

        public async Task<byte[]> GeneratePdfReport(int userId)
        {
            var expenses = await _context.Expenses
                .Include(e => e.Category)
                .Where(e => e.UserId == userId)
                .ToListAsync();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    
                    page.Header().Column(col =>
                    {
                        col.Item().Text("Expense Tracker Report").FontSize(20).Bold();
                        col.Item().Text($"Generated: {DateTime.Now:dd/MM/yyyy}");
                    });

                    
                    page.Content().PaddingVertical(20).Column(column =>
                    {
                        column.Spacing(10);

                        
                        column.Item().Row(row =>
                        {
                            row.RelativeItem(3).Text("Category").Bold();
                            row.RelativeItem(2).Text("Amount").Bold();
                            row.RelativeItem(5).Text("Description").Bold();
                        });

               
                        foreach (var expense in expenses)
                        {
                            column.Item().Row(row =>
                            {
                                row.RelativeItem(3).Text(expense.Category.Name);
                                row.RelativeItem(2).Text($"₦{expense.Amount}");
                                row.RelativeItem(5).Text(expense.Description);
                            });
                        }

                        column.Spacing(20);

                       
                        column.Item().Text($"Total Expenses: ₦{expenses.Sum(e => e.Amount)}").Bold();
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}