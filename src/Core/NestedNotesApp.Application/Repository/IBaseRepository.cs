using NestedNotesApp.Domain.Common;

namespace NestedNotesApp.Application.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
    }
}
