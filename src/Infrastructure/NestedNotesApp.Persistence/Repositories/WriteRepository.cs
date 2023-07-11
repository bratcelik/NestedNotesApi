using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NestedNotesApp.Application.Repository;
using NestedNotesApp.Domain.Common;
using NestedNotesApp.Persistence.Context;

namespace NestedNotesApp.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public DbSet<T> Table => _context.Set<T>();

        public WriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Unchanged;

            //    EntityEntry<T> entityEntry = await Table.AddAsync(model);
            //    return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities);
            return await _context.SaveChangesAsync() > 0;

            //    await Table.AddRangeAsync(datas);
            //    return true;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            EntityEntry<T> entityEntry = Table.Update(entity);
            await _context.SaveChangesAsync();
            return entityEntry.State == EntityState.Modified || entityEntry.State == EntityState.Unchanged;

            //    EntityEntry entityEntry = Table.Update(model);
            //    return entityEntry.State == EntityState.Modified;
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            EntityEntry entityEntry = Table.Remove(entity);
            await _context.SaveChangesAsync();
            return entityEntry.State == EntityState.Deleted || entityEntry.State == EntityState.Unchanged;
            //    T model =  await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
            //    return Remove(model);
        }

        public async Task<bool> RemoveRangeAsync(List<T> entities)
        {
            Table.RemoveRange(entities);
            return await _context.SaveChangesAsync() > 0;

            //    Table.RemoveRange(datas);
            //    return true;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
