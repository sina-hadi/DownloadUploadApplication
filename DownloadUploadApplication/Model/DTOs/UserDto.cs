using System.ComponentModel.DataAnnotations;

namespace DownloadUploadApplication.Model.DTOs
{
    public class UserDto
    {
        public required string Name { get; set; }
        public IFormFile? File { get; set; }
    }
}
