using System;
using MediatR;

namespace Notes.Application.Notes.Commands.CreateNote
{
    // класс, реализующий интерфейс библиотеки MediatR и помечающий результат выполнения команды,
    // возвращающий результат определенного типа

    // Содержит информацию о том, что необходимо для создания заметки
    public class CreateNoteCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
