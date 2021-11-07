using CommandService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Command> Commands { get; set; }
        public DbSet<PlatformModel> Platforms { get; set; }
    }
}