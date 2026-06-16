using ExpenseTracker.DTOs;
using ExpenseTracker.Models;
using ExpenseTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController(ExpenseService _expenseService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ExpenseResponseDto>>> GetAllExpenses()
        {
            var expenses = await _expenseService.GetAllExpenses();

             List<ExpenseResponseDto> response = expenses                            
                .Select(e => new ExpenseResponseDto
            {
                Id = e.Id,
                Amount = e.Amount,
                Description = e.Description,
                Date = e.Date,
                UserId = e.UserId,
                User = new UserPreviewDto
                {
                    FirstName = e.User.FirstName,
                    LastName = e.User.LastName,
                },
                CategoryId = e.CategoryId,
                Category = new CategoryPreviewDto
                {
                    Id = e.Category.Id,
                    Name = e.Category.Name
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
                UserId = expense.UserId,
                CategoryId = expense.CategoryId,
                User = new UserPreviewDto
                {
                    FirstName = expense.User.FirstName,
                    LastName = expense.User.LastName,
                },
                Category = new CategoryPreviewDto
                {
                    Id = expense.Category.Id,
                    Name = expense.Category.Name
                }
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseResponseDto>> CreateExpense(CreateExpenseDto expenseDto)
        {
            var newExpense = await _expenseService.AddExpense(expenseDto.Amount, expenseDto.Description, expenseDto.UserId, expenseDto.CategoryName.ToLower());
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
                UserId = newExpense.UserId,
                CategoryId = newExpense.CategoryId,
                User = new UserPreviewDto
                {
                    FirstName = newExpense.User.FirstName,
                    LastName = newExpense.User.LastName,
                },
                Category = new CategoryPreviewDto
                {
                    Id = newExpense.Category.Id,
                    Name = newExpense.Category.Name
                }
            };

            return CreatedAtAction(nameof(GetExpenseById), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ExpenseResponseDto>> UpdateExpense(int id, UpdateExpenseDto updateDto)
        {

            var updatedExpense = await _expenseService.UpdateExpense(id, updateDto.Amount, updateDto.Description, updateDto.CategoryName.ToLower());

            if (updatedExpense == null)
            {
                return BadRequest("Expense not found, or invalid Category specified.");
            }

            var response = new ExpenseResponseDto
            {
                Id = updatedExpense.Id,
                Amount = updatedExpense.Amount,
                Description = updatedExpense.Description,
                Date = updatedExpense.Date,
                UserId = updatedExpense.UserId,
                CategoryId = updatedExpense.CategoryId,
                User = new UserPreviewDto
                {
                    FirstName = updatedExpense.User.FirstName,
                    LastName = updatedExpense.User.LastName,
                },
                Category = new CategoryPreviewDto
                {
                    Id = updatedExpense.Category.Id,
                    Name = updatedExpense.Category.Name
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