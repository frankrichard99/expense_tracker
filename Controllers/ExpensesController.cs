using ExpenseTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController(ExpenseService _expenseService) : ControllerBase
    {
        [HttpGet]
        public ActionResult GetAllExpenses()
        {
            return Ok(_expenseService.GetAllExpenses());
        }

        [HttpGet("{id}")]
        public ActionResult GetExpenseById(int id)
        {
            var expense = _expenseService.GetExpenseById(id);
            if(expense == null)
            {
                return NotFound();
            }
           return Ok(expense);
        }
        
    }
}
