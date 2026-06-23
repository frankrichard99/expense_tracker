using System.Security.Claims;
using ExpenseTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportsController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [Authorize]
        [HttpGet("pdf")]
        public async Task<IActionResult> DownloadPdf()
        {
            var userIdClaim =
                User.FindFirst(
                    ClaimTypes.NameIdentifier)?.Value;

            int userId = int.Parse(userIdClaim!);

            var pdf = await _reportService.GeneratePdfReport(userId);

            return File(
                pdf,
                "application/pdf",
                "expense-report.pdf");
        }
    }


}
