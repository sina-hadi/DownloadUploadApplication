using DownloadUploadApplication.Model;
using DownloadUploadApplication.Model.DTOs;
using DownloadUploadApplication.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace DownloadUploadApplication.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUploadDownloadService _uploadDownloadService;
        private readonly ISqlService _sqlService;

        public FileController(IWebHostEnvironment webHostEnvironment, IUploadDownloadService uploadDownloadService, ISqlService sqlService)
        {
            _webHostEnvironment = webHostEnvironment;
            _uploadDownloadService = uploadDownloadService;
            _sqlService = sqlService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] UserDto userDto)
        {
            string? filePath = await _uploadDownloadService.UploadFileAsync(userDto);
            if (filePath == null)
            {
                return BadRequest("Can't upload file");
            }

            bool addUser = await _sqlService.AddUserAsync(
                new User
                {
                    Name = userDto.Name,
                    FileDirection = filePath
                });

            if (!addUser)
            {
                return BadRequest("Can't add user");
            }

            return Ok(filePath);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Download([FromRoute] int id)
        {
            try
            {
                User? user = await _sqlService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return BadRequest("Cannot Find User!");
                }

                if (user.FileDirection == null)
                {
                    return Ok("User don't have file!");
                }
                byte[]? file = await _uploadDownloadService.DownloadFileAsync(user.FileDirection);
                if (file == null)
                {
                    return BadRequest("Cannot Download File!");
                }

                return File(file, "application/force-download", "Sample");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}