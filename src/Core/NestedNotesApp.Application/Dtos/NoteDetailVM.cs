namespace NestedNotesApp.Application.Dtos
{
    public class NoteDetailVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid ParentId { get; set; }

        public HashSet<NoteDetailVM> Notes { get; set; }
    }
}
