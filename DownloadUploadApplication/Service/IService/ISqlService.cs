using DownloadUploadApplication.Model;

namespace DownloadUploadApplication.Service.IService
{
    public interface ISqlService
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<bool> AddUserAsync(User user);
    }
}
