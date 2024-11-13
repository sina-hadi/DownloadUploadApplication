using System.ComponentModel.DataAnnotations;

namespace DownloadUploadApplication.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? FileDirection {  get; set; }
    }
}
