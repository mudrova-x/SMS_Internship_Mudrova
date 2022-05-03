using Notes.Test.Common;
using System.Threading;
using System.Threading.Tasks;
using Notes.Application.Notes.Commands.CreateNote;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace Notes.Test.Notes.Commands
{
    public class CreateNoteCommandHandlerTests : TestCommandBase
    {
        public async Task CreateNoteCommandHandler_Success()
        {
            //Arrange - подготовка данных для теста
            var handler = new CreateNoteCommandHandler(Context);
            var noteName = "note name";
            var noteDetails = "note details";

            //Act - выполнение логики
            var noteId = await handler.Handle(
                new CreateNoteCommand
                {
                    Title = noteName,
                    Details = noteDetails,
                    UserId = NotesContextFactory.UserAId
                }, CancellationToken.None);

            //Assert - проверка результатов
            Assert.NotNull(
                await Context.Notes.SingleOrDefaultAsync(note =>
                note.Id == noteId && note.Title == noteName && note.Details == noteDetails));
        }
    }
}
