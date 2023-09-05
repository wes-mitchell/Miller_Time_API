using Microsoft.EntityFrameworkCore;
using MillerTime.API.Models.DBModels;

namespace MillerTime.API.Context
{
    public class MillerTimeContext : DbContext
    {
        public MillerTimeContext(DbContextOptions<MillerTimeContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Video> Videos { get; set; }

    }
}
