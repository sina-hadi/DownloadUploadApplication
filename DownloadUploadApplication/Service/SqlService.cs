using DownloadUploadApplication.Data;
using DownloadUploadApplication.Model;
using DownloadUploadApplication.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace DownloadUploadApplication.Service
{
    public class SqlService : ISqlService
    {
        private readonly AppDbContext _db;

        public SqlService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
            } catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            User? user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }
    }
}
