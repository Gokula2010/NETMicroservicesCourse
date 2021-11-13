using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;
using Serilog;

namespace PlatformService.Data
{
    public static class DataSeeder
    {
        private static ILogger _logger;
        public static void SeedData(ILogger loggerFactory, IApplicationBuilder app)
        {
            _logger = loggerFactory;

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                Data(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void Data(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                _logger.Information("=> Seeding data.");

                context.Platforms.AddRange(
                    new PlatformModel() { Name = "DOT NET", Publisher = "Microsoft", Cost = "Free" },
                    new PlatformModel() { Name = "SQL Server", Publisher = "Microsoft", Cost = "Free" },
                    new PlatformModel() { Name = "Nodejs", Publisher = "Node.org", Cost = "Free" }
                );

                context.SaveChanges();
                _logger.Information("Data seed completed.");
            }
            else
            {
                _logger.Warning("We already have data");
            }
        }
    }
}