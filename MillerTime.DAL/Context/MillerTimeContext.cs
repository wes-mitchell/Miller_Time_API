using Microsoft.EntityFrameworkCore;
using MillerTime.Models.DBModels;

namespace MillerTime.DAL.Context
{
    public class MillerTimeContext : DbContext
    {
        public MillerTimeContext(DbContextOptions<MillerTimeContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Video>().ToTable("Videos");
        }
    }
}
