namespace NestedNotesApp.Application.Dtos
{
    public class NotesVM
    {
        public NotesVM()
        {
            Notes = new HashSet<NotesVM>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public HashSet<NotesVM> Notes { get; set; }

    }
}
