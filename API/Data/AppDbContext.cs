using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<UserApp> Users { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
