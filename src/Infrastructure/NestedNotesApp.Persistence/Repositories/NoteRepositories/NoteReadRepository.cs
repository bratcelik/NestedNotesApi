using NestedNotesApp.Application.Repository.NoteRepositories;
using NestedNotesApp.Domain.Entities;
using NestedNotesApp.Persistence.Context;

namespace NestedNotesApp.Persistence.Repositories.NoteRepositories
{
    public class NoteReadRepository : ReadRepository<Note>, INoteReadRepository
    {
        public NoteReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
