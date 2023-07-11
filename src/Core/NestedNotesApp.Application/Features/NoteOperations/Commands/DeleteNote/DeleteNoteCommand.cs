using MediatR;
using NestedNotesApp.Application.Repository.NoteRepositories;
using NestedNotesApp.Application.Wrappers;

namespace NestedNotesApp.Application.Features.NoteOperations.Commands.DeleteNote
{
    public class DeleteNoteCommand : IRequest<ServiceResponse<Guid>>
    {

        public Guid Id { get; set; }

        public DeleteNoteCommand(string id)
        {
            Id = Guid.Parse(id);
        }
    }

    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, ServiceResponse<Guid>>
    {
        private readonly INoteWriteRepository _noteWriteRepository;
        private readonly INoteReadRepository _noteReadRepository;

        public DeleteNoteCommandHandler(INoteWriteRepository noteWriteRepository, INoteReadRepository noteReadRepository)
        {
            _noteWriteRepository = noteWriteRepository;
            _noteReadRepository = noteReadRepository;
        }

        public async Task<ServiceResponse<Guid>> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteReadRepository.GetByIdAsync(request.Id);

            if (note is null)
                throw new InvalidOperationException("Not bulunamadı!");

            

            ServiceResponse<Guid> sp = new ServiceResponse<Guid>(note.Id);
            sp.IsSuccess = await DeleteNoteAndChildren(note.Id);
            sp.Message = "Başarılı!";

            return sp;
        }

        private async Task<bool> DeleteNoteAndChildren(Guid noteId)
        {
            var includes = new string[] { "Notes" };

            var note = await _noteReadRepository.GetSingleAsync(n => n.Id == noteId, includes);

            if (note is null)
                return true;

            foreach (var childNote in note.Notes)
            {
                return await DeleteNoteAndChildren(childNote.Id);
            }

            return await _noteWriteRepository.RemoveAsync(note);
        }
    }
}
