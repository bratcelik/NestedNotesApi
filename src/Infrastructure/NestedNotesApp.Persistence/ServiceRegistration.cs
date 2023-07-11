using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NestedNotesApp.Application.Repository.NoteRepositories;
using NestedNotesApp.Persistence.Context;
using NestedNotesApp.Persistence.Repositories.NoteRepositories;

namespace NestedNotesApp.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceRegistration(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("memoryDb"));

            services.AddTransient<INoteWriteRepository, NoteWriteRepository>();
            services.AddTransient<INoteReadRepository, NoteReadRepository>();

        }
    }
}
