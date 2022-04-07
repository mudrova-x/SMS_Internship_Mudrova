using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notes.Domain;


namespace Notes.Application.Interfaces
{
    public interface INotesDbContext
    {
        DbSet<Note> Notes { get; set; } // коллекция всех сущностей в контексте (или запрашивается из бд заданного типа)
        Task<int> SaveChangesAsync(CancellationToken cancellationToken); // сохраняет изменения контекста в базу данных
    }
}
