using AutoMapper;
using MediatR;
using NestedNotesApp.Application.Dtos;
using NestedNotesApp.Application.Repository.NoteRepositories;
using NestedNotesApp.Application.Wrappers;
using NestedNotesApp.Domain.Entities;

namespace NestedNotesApp.Application.Features.NoteOperations.Commands.CreateNote
{

    public class CreateNoteCommand : IRequest<ServiceResponse<Guid>>
    {
        public CreateNoteModel Model { get; set; }

    }

    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, ServiceResponse<Guid>>
    {
        private readonly INoteWriteRepository _noteWriteRepository;
        private readonly IMapper _mapper;

        public CreateNoteCommandHandler(INoteWriteRepository noteWriteRepository, INoteReadRepository noteReadRepository, IMapper mapper)
        {
            _noteWriteRepository = noteWriteRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Guid>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {

            var note = _mapper.Map<Note>(request.Model);

            ServiceResponse<Guid> sp = new ServiceResponse<Guid>(Guid.Empty);
            sp.IsSuccess = await _noteWriteRepository.AddAsync(note);
            sp.Value = note.Id;
            sp.Message = "Başarılı!";

            return sp;

        }
    }
}
