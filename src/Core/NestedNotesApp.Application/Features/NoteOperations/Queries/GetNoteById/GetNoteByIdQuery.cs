using AutoMapper;
using MediatR;
using NestedNotesApp.Application.Dtos;
using NestedNotesApp.Application.Repository.NoteRepositories;
using NestedNotesApp.Application.Wrappers;
using NestedNotesApp.Domain.Entities;

namespace NestedNotesApp.Application.Features.NoteOperations.Queries.GetNoteById
{
    public class GetNoteByIdQuery : IRequest<ServiceResponse<NotesVM>>
    {
        public readonly Guid Id;

        public GetNoteByIdQuery(string id)
        {
            Id = Guid.Parse(id);
        }
    }

    public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, ServiceResponse<NotesVM>>
    {
        private readonly INoteReadRepository _noteReadRepository;
        private readonly IMapper _mapper;

        public GetNoteByIdQueryHandler(INoteReadRepository noteReadRepository, IMapper mapper)
        {
            _noteReadRepository = noteReadRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<NotesVM>> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
        {

            var note = await _noteReadRepository.GetSingleAsync(x=> x.Id == request.Id);
            //var note = await _noteReadRepository.GetWhereAsync(x => x.Id == request.Id);

            note.Notes = await CreateNestedNotes(request.Id);



            //var nestedNotes = await CreateNestedNotes(null);

            var vm = _mapper.Map<NotesVM>(note);


            ServiceResponse<NotesVM> sp = new ServiceResponse<NotesVM>();
            sp.IsSuccess = true;
            sp.Message = "Başarılı!";
            sp.Value = vm;

            return sp;
        }

        private async Task<List<Note>> CreateNestedNotes(Guid? rootId)
        {
            var nestedNotes = new List<Note>();

            //var childNotes = notes.Where(n=> n.ParentId == rootId).ToList();
            var childNotes = await _noteReadRepository.GetWhereAsync(x => x.ParentId == rootId);


            foreach (var note in childNotes)
            {
                var nestedNote = new Note
                {
                    Id = note.Id,
                    Title = note.Title,
                    Content = note.Content,
                    Notes = await CreateNestedNotes(note.Id)
                };

                nestedNotes.Add(nestedNote);
            }

            return nestedNotes;

        }
    }
}
