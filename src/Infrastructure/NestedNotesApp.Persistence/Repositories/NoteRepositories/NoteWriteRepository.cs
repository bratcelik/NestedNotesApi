using NestedNotesApp.Application.Repository.NoteRepositories;
using NestedNotesApp.Domain.Entities;
using NestedNotesApp.Persistence.Context;

namespace NestedNotesApp.Persistence.Repositories.NoteRepositories
{
    public class NoteWriteRepository : WriteRepository<Note>, INoteWriteRepository
    {
        public NoteWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
