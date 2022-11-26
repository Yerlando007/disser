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
        public DbSet<GOST>? GOST { get; set; }
        public DbSet<GOSTKeyWords>? GOSTKeyWords { get; set; }
        public DbSet<OtherGOST>? OtherGOST { get; set; }
        public DbSet<RukovoditelWantWork>? RukovoditelWantWork { get; set; }
        public DbSet<SimilarGOST>? SimilarGOST { get; set; }
        public DbSet<UsersGosts>? UsersGosts { get; set; }
    }
}
