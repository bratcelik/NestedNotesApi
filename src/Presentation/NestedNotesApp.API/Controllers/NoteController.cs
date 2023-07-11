using MediatR;
using Microsoft.AspNetCore.Mvc;
using NestedNotesApp.Application.Dtos;
using NestedNotesApp.Application.Features.NoteOperations.Commands.CreateNote;
using NestedNotesApp.Application.Features.NoteOperations.Commands.DeleteNote;
using NestedNotesApp.Application.Features.NoteOperations.Commands.UpdateNote;
using NestedNotesApp.Application.Features.NoteOperations.Queries.GetAllNotes;
using NestedNotesApp.Application.Features.NoteOperations.Queries.GetNoteById;

namespace NestedNotesApp.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllNotesQuery();
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var query = new GetNoteByIdQuery(id);
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateNoteModel model)
        {
            CreateNoteCommand command = new CreateNoteCommand();
            command.Model = model;
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(string id, [FromBody] UpdateNoteModel model)
        {
            UpdateNoteCommand command = new UpdateNoteCommand(id);
            command.Model = model;
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            var command = new DeleteNoteCommand(id);
            return Ok(await _mediator.Send(command));
        }
    }
}
