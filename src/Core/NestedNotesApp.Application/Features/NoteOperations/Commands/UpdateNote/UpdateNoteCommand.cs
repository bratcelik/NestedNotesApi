using MediatR;
using NestedNotesApp.Application.Dtos;
using NestedNotesApp.Application.Repository.NoteRepositories;
using NestedNotesApp.Application.Wrappers;

namespace NestedNotesApp.Application.Features.NoteOperations.Commands.UpdateNote
{
    public class UpdateNoteCommand : IRequest<ServiceResponse<Guid>>
    {
        public readonly Guid Id;
        public UpdateNoteModel Model { get; set; }

        public UpdateNoteCommand(string id)
        {
            Id = Guid.Parse(id);
        }
    }

    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, ServiceResponse<Guid>>
    {
        private readonly INoteWriteRepository _noteWriteRepository;
        private readonly INoteReadRepository _noteReadRepository;

        public UpdateNoteCommandHandler(INoteWriteRepository noteWriteRepository, INoteReadRepository noteReadRepository)
        {
            _noteWriteRepository = noteWriteRepository;
            _noteReadRepository = noteReadRepository;
        }

        public async Task<ServiceResponse<Guid>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteReadRepository.GetByIdAsync(request.Id);

            if (note is null)
                throw new InvalidOperationException("Güncellenecek Not Bulunamadı!");

            if (await _noteReadRepository.AnyAsync(x => x.Title.ToLower() == request.Model.Title.ToLower() && x.Id != note.Id))
                throw new InvalidOperationException("Aynı isimli bir not zaten mevcut!");

            note.Title = String.IsNullOrEmpty(request.Model.Title.Trim()) ? note.Title : request.Model.Title.Trim();
            note.Content = String.IsNullOrEmpty(request.Model.Content.Trim()) ? note.Content : request.Model.Content.Trim();

            ServiceResponse<Guid> sp = new ServiceResponse<Guid>(note.Id);
            sp.IsSuccess = await _noteWriteRepository.UpdateAsync(note);
            sp.Message = "Başarılı!";

            return sp;
        }
    }
}
