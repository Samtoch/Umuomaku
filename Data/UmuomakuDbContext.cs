using Microsoft.EntityFrameworkCore;
using Umuomaku.Data.Models;

namespace Umuomaku.Data
{
    public class UmuomakuDbContext : DbContext
    {
        public UmuomakuDbContext(DbContextOptions<UmuomakuDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Highlight> Highlights { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
    }
}
