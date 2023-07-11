namespace NestedNotesApp.Application.Wrappers
{
    public class PagedResponse<T> : ServiceResponse<T>
    {
        public int TotalCount { get; set; }

        public PagedResponse(T value, int totalCount): base(value)
        {
            TotalCount = totalCount;
        }

        public PagedResponse()
        {
            
        }
    }
}
