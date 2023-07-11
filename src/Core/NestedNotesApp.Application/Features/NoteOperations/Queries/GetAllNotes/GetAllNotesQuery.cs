using AutoMapper;
using MediatR;
using NestedNotesApp.Application.Dtos;
using NestedNotesApp.Application.Repository.NoteRepositories;
using NestedNotesApp.Application.Wrappers;
using NestedNotesApp.Domain.Entities;

namespace NestedNotesApp.Application.Features.NoteOperations.Queries.GetAllNotes
{
    public class GetAllNotesQuery : IRequest<ServiceResponse<List<NotesVM>>>
    {

    }

    public class GetAllNotesQueryHandler : IRequestHandler<GetAllNotesQuery, ServiceResponse<List<NotesVM>>>
    {
        private readonly INoteReadRepository _noteReadRepository;
        private readonly IMapper _mapper;

        public GetAllNotesQueryHandler(INoteReadRepository noteReadRepository, IMapper mapper)
        {
            _noteReadRepository = noteReadRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<NotesVM>>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
        {
            //var includes = new string[] { "Notes" };

            var noteList = await _noteReadRepository.GetWhereAsync(x => x.ParentId == null);

            var nestedNotes = await CreateNestedNotes(null, noteList);

            foreach (var note in noteList)
            {
                note.Notes = await _noteReadRepository.GetWhereAsync(x=> x.ParentId == note.Id);
            }
            //var nestedNotes = await CreateNestedNotes(null);

            var vm = _mapper.Map<List<NotesVM>>(nestedNotes);

            ServiceResponse<List<NotesVM>> sp = new ServiceResponse<List<NotesVM>>();
            sp.IsSuccess = true;
            sp.Message = "Başarılı!";
            sp.Value = vm;

            return sp;
        }

        private async Task<List<Note>> CreateNestedNotes(Guid? rootId, List<Note> notes)
        {
            var nestedNotes = new List<Note>();

            //var childNotes = notes.Where(n=> n.ParentId == rootId).ToList();
            var childNotes = await _noteReadRepository.GetWhereAsync(x=> x.ParentId == rootId);


            foreach (var note in childNotes)
            {
                var nestedNote = new Note
                {
                    Id = note.Id,
                    Title = note.Title,
                    Content = note.Content,
                    Notes = await CreateNestedNotes(note.Id, notes)
                };

                nestedNotes.Add(nestedNote);
            }

            return nestedNotes;

        }
    }
}
