using Microsoft.EntityFrameworkCore;
using disser.Models.EF.Users;
using disser.Models.EF.GOST;

namespace disser.Models.Base
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User>? Users { get; set; }
        public DbSet<Documents>? Documents { get; set; }
        public DbSet<CreatedGOST>? GOST { get; set; }
        public DbSet<AllGOST>? OtherGOST { get; set; }
        public DbSet<RukovoditelWantWork>? RukovoditelWantWork { get; set; }
    }
}
