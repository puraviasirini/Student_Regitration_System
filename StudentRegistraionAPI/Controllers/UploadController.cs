using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentRegistraionAPI.Data.Interfaces;

namespace StudentRegistraionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IFileUploadServiceRepository _fileUploadService;

        public UploadController(IFileUploadServiceRepository fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        private readonly string _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        [HttpPost("profile-image")]
        public async Task<IActionResult> UploadProfileImage([FromForm] IFormFile image)
        {
            try
            {
                var relativePath = await _fileUploadService.UploadProfileImage(image);
                return Ok(relativePath);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        
    }
}
