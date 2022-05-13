//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();
//app.MapGet("/", () => "Hello World!");
//app.Run();

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Notes.Persistence;
using Serilog;
using Serilog.Events;

namespace Notes.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // инициализируем для логирования в файл NotesWebAppLogFile
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.File("NotesWebAppLogFile-.txt", rollingInterval:
                    RollingInterval.Day)
                .CreateLogger();
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                //вызов метода инициализации базы
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
                    DbInitializer.Initialize(context);

                }
                catch (Exception exeption)
                {
                    Log.Fatal(exeption, "An error occurred while app initialization");
                }

            }

            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(WebApplicationBuilder =>
            {
                WebApplicationBuilder.UseStartup<Startup>();
            });
    }
}