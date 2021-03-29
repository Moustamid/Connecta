using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pastel;
using Persistence;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
           var host =  CreateHostBuilder(args).Build();
           
           // Dependency Injection
           using var scope = host.Services.CreateScope();

           var services = scope.ServiceProvider;
  
           try {
               var context = services.GetRequiredService<DataContext>();
               // Migration 
               await context.Database.MigrateAsync();
               // Seeding
               await Seed.SeedData(context);

               Console.WriteLine($"{"Migration created".Pastel(Color.FromArgb(247, 202, 24))}");
           }catch (Exception ex) {
               var logger = services.GetRequiredService<ILogger<Program>>();
               logger.LogError(ex , "An error occured during migration");
               Console.WriteLine($"{"An error occured during migration".Pastel(Color.FromArgb(207, 0, 15))}");
           }

           await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
