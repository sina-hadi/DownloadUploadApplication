using DownloadUploadApplication.Model.DTOs;
using DownloadUploadApplication.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace DownloadUploadApplication.Service
{
    public class UploadDownloadService : IUploadDownloadService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadDownloadService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Byte[]?> DownloadFileAsync(string fileName)
        {
            if (fileName != null)
            {
                try
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(fileName);

                    return await Task.FromResult(fileBytes);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<string?> UploadFileAsync(UserDto userDto)
        {
            if (userDto.File != null && userDto.File.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\Files\\"))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\Files\\");
                    }

                    var path = _webHostEnvironment.WebRootPath + "\\Files\\" +
                        Path.GetFileNameWithoutExtension(userDto.File.FileName) +
                        "-" + Guid.NewGuid() + Path.GetExtension(userDto.File.FileName);

                    using (FileStream fileStream = System.IO.File.Create(path))
                    {
                        userDto.File.CopyTo(fileStream);
                        fileStream.Flush();
                        return await Task.FromResult(path);
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
