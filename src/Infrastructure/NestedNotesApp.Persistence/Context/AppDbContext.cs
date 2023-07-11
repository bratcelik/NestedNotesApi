using Microsoft.EntityFrameworkCore;
using NestedNotesApp.Domain.Entities;

namespace NestedNotesApp.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }

    }
}
