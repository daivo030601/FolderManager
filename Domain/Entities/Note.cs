using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Domain.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public int? FolderId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Folder Folder { get; set; }
    }
}
