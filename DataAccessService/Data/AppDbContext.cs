using Microsoft.EntityFrameworkCore;
using DataAccessService.Models;

namespace DataAccessService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Models.Route> Routes { get; set; }
    }
}
