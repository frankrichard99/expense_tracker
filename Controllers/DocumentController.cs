using ExpenseTracker.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
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

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };

            var extension = Path.GetExtension(dto.File.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Invalid file type");
            }

            if (dto.File.Length > 5_000_000)
            {
                return BadRequest(
                    "Maximum size is 5MB");
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{dto.File.FileName}";

            var filePath =
                Path.Combine(
                    uploadsFolder,
                    uniqueFileName);

           
            using var stream =
                new FileStream(
                    filePath,
                    FileMode.Create);


            await dto.File.CopyToAsync(stream);

            return Ok(new
            {
                FileName = uniqueFileName,
                OriginalName = dto.File.FileName,
                Size = dto.File.Length
            });
        }
    }
}
