using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedNotesApp.Application.Dtos
{
    public class CreateNoteModel
    {
        public Guid? ParentId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
