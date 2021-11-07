using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.SystemConsole.Themes;

namespace PlatformService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                            .Enrich.FromLogContext()
                            .WriteTo.Console(
                                theme: SystemConsoleTheme.Literate,
                                outputTemplate: "{Timestamp:HH:mm} [{Level}] {Message}{NewLine}{Exception}")
                            // .WriteTo.AzureEventHub(new RenderedCompactJsonFormatter(), "EventHub Connection String", "EventHub Name")
                            .CreateLogger();

            // To enable Serilog Self Diagnostics 
            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine($"Serilog Error: {msg}"));
            
            try
            {
                Log.Information("Starting Up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, services, configuration) =>
                {
                    configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .WriteTo.Console(new RenderedCompactJsonFormatter())
                    // .WriteTo.AzureEventHub(new RenderedCompactJsonFormatter(), "EventHub Connection String", "EventHub Name")
                    // .WriteTo.Console(
                    //     theme: SystemConsoleTheme.Colored,
                    //     outputTemplate: "{Timestamp:HH:mm} [{Level}] {Message}{NewLine}{Exception}"
                    // )
                    ;

                    // To enable Serilog Self Diagnostics 
                    // Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine($"Serilog Error - : {msg}"));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
