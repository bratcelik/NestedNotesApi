using NestedNotesApp.Domain.Common;

namespace NestedNotesApp.Application.Repository
{
    public interface IWriteRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        Task<bool> UpdateAsync(T entity);
        Task<bool> RemoveAsync(T entity);
        Task<bool> RemoveRangeAsync(List<T> entities);
    }
}
