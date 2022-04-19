﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Interfaces;

namespace Notes.Persistence
{
    // метод расширения для добавления контекста бд в приложение (добавляет и регистрирует)
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection 
            services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<NotesDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
            services.AddScoped<INotesDbContext>(provider =>
            provider.GetService<NotesDbContext>());
            return services;
        }
    }
}
