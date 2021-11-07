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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlatformModel>()
                .HasMany(x => x.Commands)
                .WithOne(x => x.Platform)
                .HasForeignKey(x => x.PlatformId);

            modelBuilder.Entity<Command>()
                .HasOne(x => x.Platform)
                .WithMany(x => x.Commands)
                .HasForeignKey(x => x.PlatformId);
        }
    }
}