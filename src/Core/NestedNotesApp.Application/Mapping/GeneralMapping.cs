using AutoMapper;
using NestedNotesApp.Application.Dtos;
using NestedNotesApp.Domain.Entities;

namespace NestedNotesApp.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            #region Note Mapping
            CreateMap<Note, NotesVM>();
            CreateMap<Note, NoteDetailVM>();
            CreateMap<CreateNoteModel, Note>();
            #endregion
        }
    }
}
