using DownloadUploadApplication.Model.DTOs;

namespace DownloadUploadApplication.Service.IService
{
    public interface IUploadDownloadService
    {
        Task<string?> UploadFileAsync(UserDto user);
        Task<byte[]?> DownloadFileAsync(string filePath);
    }
}
