using Microsoft.EntityFrameworkCore;
using disser.Models.EF;

namespace disser.Models.Base
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User>? Users { get; set; }
    }
}
