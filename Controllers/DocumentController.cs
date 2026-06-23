using ExpenseTracker.DTOs;
using ExpenseTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController(CloudinaryService _cloudinaryService) : ControllerBase
    {

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] UploadDocumentDto dto)
        {
           
            string newFile = await _cloudinaryService.UploadFile(dto.File);

            if(newFile == null)
            {
                return BadRequest("Please check the file extension & size");
            }

            return Ok(new
            {
                FileUrl = newFile,
                OriginalName = dto.File.FileName,
                //Size = dto.File.Length
            });
        }
    }
}
