//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();
//app.MapGet("/", () => "Hello World!");
//app.Run();

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Notes.Persistence;

namespace Notes.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
                }

            }

            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(WebApplicationBuilder =>
            {
                WebApplicationBuilder.UseStartup<Startup>();
            });
    }
}