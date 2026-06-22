using ExpenseTracker.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadDocumentDto dto)
        {
            var uploadsFolder =
                    Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "Uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath =
                Path.Combine(
                    uploadsFolder,
                    dto.File.FileName);

            using var stream =
                new FileStream(
                    filePath,
                    FileMode.Create);

            await dto.File.CopyToAsync(stream);

            return Ok();
        }
    }
}
