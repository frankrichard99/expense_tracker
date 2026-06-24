using ExpenseTracker.DTOs;
using ExpenseTracker.Models;
using ExpenseTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController(ExpenseService _expenseService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ExpenseResponseDto>>> GetAllExpenses()
        {
            var expenses = await _expenseService.GetAllExpenses();

            List<ExpenseResponseDto> response = expenses.Select(e => new ExpenseResponseDto
            {
                Id = e.Id,
                Amount = e.Amount,
                Description = e.Description,
                Date = e.Date,
                UserId = e.UserId,
                ReceiptUrl = e.ReceiptUrl,
                User = new UserPreviewDto
                {
                    FirstName = e.User?.FirstName ?? "Unknown",
                    LastName = e.User?.LastName ?? "User",
                },
                CategoryId = e.CategoryId,
                Category = new CategoryPreviewDto
                {
                    Id = e.Category?.Id ?? 0,
                    Name = e.Category?.Name ?? "Uncategorized"
                }
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseResponseDto>> GetExpenseById(int id)
        {
            var expense = await _expenseService.GetExpenseById(id);
            if (expense == null)
            {
                return NotFound();
            }

            var response = new ExpenseResponseDto
            {
                Id = expense.Id,
                Amount = expense.Amount,
                Description = expense.Description,
                Date = expense.Date,
                ReceiptUrl = expense.ReceiptUrl,
                UserId = expense.UserId,
                CategoryId = expense.CategoryId,
                User = new UserPreviewDto
                {
                    FirstName = expense.User?.FirstName ?? "Unknown",
                    LastName = expense.User?.LastName ?? "User",
                },
                Category = new CategoryPreviewDto
                {
                    Id = expense.Category?.Id ?? 0,
                    Name = expense.Category?.Name ?? "Uncategorized"
                }
            };

            return Ok(response);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ExpenseResponseDto>> CreateExpense([FromForm] CreateExpenseDto expenseDto) // 🚀 Added [FromForm]
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(userIdClaim!);

            var newExpense = await _expenseService.AddExpense(
                expenseDto.Amount,
                expenseDto.Description,
                userId,
                expenseDto.CategoryName.ToLower(),
                expenseDto.ReceiptImage
            );

            if (newExpense == null)
            {
                return BadRequest("Invalid User or Category");
            }

            var response = new ExpenseResponseDto
            {
                Id = newExpense.Id,
                Amount = newExpense.Amount,
                Description = newExpense.Description,
                Date = newExpense.Date,
                ReceiptUrl = newExpense.ReceiptUrl,
                UserId = newExpense.UserId,
                CategoryId = newExpense.CategoryId,
                User = new UserPreviewDto
                {
                    FirstName = newExpense.User?.FirstName ?? "Unknown",
                    LastName = newExpense.User?.LastName ?? "User",
                },
                Category = new CategoryPreviewDto
                {
                    Id = newExpense.Category?.Id ?? 0,
                    Name = newExpense.Category?.Name ?? "Uncategorized"
                }
            };

            return CreatedAtAction(nameof(GetExpenseById), new { id = response.Id }, response);
        }

        [HttpGet("my")]
        public async Task<ActionResult> GetMyExpenses()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(userIdClaim!);

            var expenses = await _expenseService.GetExpensesByUserId(userId);

            List<ExpenseResponseDto> response = expenses.Select(e => new ExpenseResponseDto
            {
                Id = e.Id,
                Amount = e.Amount,
                Description = e.Description,
                Date = e.Date,
                UserId = e.UserId,
                ReceiptUrl = e.ReceiptUrl,
                User = new UserPreviewDto
                {
                    FirstName = e.User?.FirstName ?? "Unknown",
                    LastName = e.User?.LastName ?? "User",
                },
                CategoryId = e.CategoryId,
                Category = new CategoryPreviewDto
                {
                    Id = e.Category?.Id ?? 0,
                    Name = e.Category?.Name ?? "Uncategorized"
                }
            }).ToList();

            return Ok(response);
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ExpenseResponseDto>> UpdateExpense(int id, [FromForm] UpdateExpenseDto updateDto) // 🚀 Added [FromForm]
        {
            var updatedExpense = await _expenseService.UpdateExpense(
                id,
                updateDto.Amount,
                updateDto.Description,
                updateDto.CategoryName?.ToLower(),
                updateDto.ReceiptImage
            );

            if (updatedExpense == null)
                return BadRequest("Expense not found, or invalid Category specified.");

            var response = new ExpenseResponseDto
            {
                Id = updatedExpense.Id,
                Amount = updatedExpense.Amount,
                Description = updatedExpense.Description,
                Date = updatedExpense.Date,
                ReceiptUrl = updatedExpense.ReceiptUrl,
                UserId = updatedExpense.UserId,
                CategoryId = updatedExpense.CategoryId,
                User = new UserPreviewDto
                {
                    FirstName = updatedExpense.User?.FirstName ?? "Unknown",
                    LastName = updatedExpense.User?.LastName ?? "User",
                },
                Category = new CategoryPreviewDto
                {
                    Id = updatedExpense.Category?.Id ?? 0,
                    Name = updatedExpense.Category?.Name ?? "Uncategorized"
                }
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var success = await _expenseService.DeleteExpense(id);
            if (!success)
            {
                return NotFound($"Expense with ID {id} not found.");
            }

            return NoContent();
        }
    }
}