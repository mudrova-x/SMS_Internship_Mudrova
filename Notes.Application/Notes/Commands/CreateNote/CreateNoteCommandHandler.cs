using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Notes.Domain;
using Notes.Application.Interfaces;
namespace Notes.Application.Notes.Commands.CreateNote
{
    // Содержит логику создания заметки
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
    {
        private readonly INotesDbContext _dbContext;

        public CreateNoteCommandHandler(INotesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // формируем заметку из запроса и возвращаем id заметки
        public async Task<Guid> Handle(CreateNoteCommand request, 
            CancellationToken cancellationToken)
        {
            var note = new Note
            {
                UserId = request.UserId,
                Title = request.Title,
                Details = request.Details,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                EditDate = null    
            };

            // добавление созданной заметки в контекст бд
            await _dbContext.Notes.AddAsync(note, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return note.Id;
        }
    }
}
