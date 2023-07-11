using NestedNotesApp.Domain.Common;

namespace NestedNotesApp.Domain.Entities
{
    public class Note : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public Note()
        {
            Notes = new HashSet<Note>();
        }


        public Note ParentNote { get; set; }
        public Guid? ParentId { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}
