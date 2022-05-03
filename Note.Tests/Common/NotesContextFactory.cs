using Microsoft.EntityFrameworkCore;
using Notes.Persistence;
using System;
using Notes.Domain.Note;

namespace Note.Tests.Common
{
    public class NotesContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid NoteUdForDelete = Guid.NewGuid();
        public static Guid NoteIdForUpdate = Guid.NewGuid();


        public static NotesDbContext Create()
        {
            var options = new DbContextOptionsBuilder<NotesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new NotesDbContext(options);
            context.Database.EnsureCreated();
            context.Notes.AddRange(
                new Note
                {
                    CreationDate = DateTime.Today,
                    Details = "Details1",
                    EditTime = null,
                    NoteIdForUpdate = Guid.Parse("C60DAA4D-9E89-45B8-B922-58A4F3C9A750"),
                    Title = "Title1",
                    UserId = UserId,
                },
                new Note
                {
                    CreationDate = DateTime.Today,
                    Details = "Details2",
                    EditTime = null,
                    Id = NoteIdForUpdate = Guid.Parse("4421CC27-0F5C-472D-AEE0-4565A3142AA7"),
                    Title = "Title2",
                    UserId = UserBId,
                },
                new Note
                {
                    CreationDate = DateTime.Today,
                    Details = "Details3",
                    EditTime = null,
                    Id = NoteIdForUpdate,
                    Title = "Title3",
                    UserId = UserAId,
                },
                new Note
                {
                    CreationDate = DateTime.Today,
                    Details = "Details4",
                    EditTime = null,
                    Id = NoteIdForUpdate,
                    Title = "Title4",
                    UserId = UserBId,
                }
                );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(NotesDbContext context)
        {

        }
    }
}
