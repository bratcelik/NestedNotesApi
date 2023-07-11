using Microsoft.EntityFrameworkCore;
using NestedNotesApp.Application.Repository;
using NestedNotesApp.Domain.Common;
using NestedNotesApp.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NestedNotesApp.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public DbSet<T> Table => _context.Set<T>();

        public ReadRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? page = null, int? pageSize = null,
            params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            int totalCount = query.Count();
            int filteredCount = totalCount;

            if (filter != null)
            {
                query = query.Where(filter);
                filteredCount = query.Count();
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            var pageData = await query.ToListAsync();

            return pageData;
        }

        public async Task<List<T>> GetAllAsync(bool tracking = true)
        {
            var query = Table.AsQueryable<T>();

            if (!tracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();

        }

        public int GetTotalCount(bool tracking = true)
        {
            var query = Table.AsQueryable<T>();

            if (!tracking)
                query = query.AsNoTracking();

            return query.Count();

        }

        public async Task<List<T>> GetAllAsync(int page, int size, bool tracking = true)
        {
            var query = Table.AsQueryable<T>().Skip(page * size).Take(size);

            if (!tracking)
                query = query.AsNoTracking();



            return await query.ToListAsync();

        }

        public async Task<List<T>> GetAllAsync(string[] includes, bool tracking = true)
        {
            var query = Table.AsQueryable<T>();

            if (!tracking)
                query = query.AsNoTracking();

            foreach (string entity in includes)
            {
                query = query.Include(entity);
            }

            return await query.ToListAsync();

        }

        public async Task<T> GetByIdAsync(Guid id, bool tracking = true)
        //=> await Table.FindAsync(Guid.Parse(id));
        //=> await Table.FirstOrDefaultAsync(data=> data.Id == Guid.Parse(id));
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(data => data.Id == id);
        }

        public async Task<Guid> FindIdAsync(Guid id)
        {
            var query = await Table.FindAsync(id);
            return query.Id;
        }

        public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if (!tracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, string[] includes, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = Table.AsNoTracking();

            foreach (string entity in includes)
            {
                query = query.Include(entity);
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = Table.AsNoTracking();

            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return await query.AnyAsync(filter);
        }
    }
}
