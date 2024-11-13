using DownloadUploadApplication.Model;
using DownloadUploadApplication.Model.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DownloadUploadApplication.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
