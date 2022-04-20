using System;
using MediatR;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListQuery : IRequest<NoteDetailsVM>
    {
        public Guid UserId { get; set; }
    }
}
