using NestedNotesApp.Domain.Common;
using System.Linq.Expressions;

namespace NestedNotesApp.Application.Repository
{
    public interface IReadRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? page = null, int? pageSize = null,
            params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetAllAsync(bool tracking = true);
        Task<List<T>> GetAllAsync(int page, int size, bool tracking = true);
        int GetTotalCount(bool tracking = true);
        Task<List<T>> GetAllAsync(string[] includes, bool tracking = true);
        Task<T> GetByIdAsync(Guid id, bool tracking = true);
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, string[] includes, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, bool tracking = true);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter, bool tracking = true);
        Task<Guid> FindIdAsync(Guid id);

    }
}
