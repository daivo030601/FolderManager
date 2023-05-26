using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Domain.Entities
{
    public class Folder
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual List<Note> Notes { get; set; }
    }
}
