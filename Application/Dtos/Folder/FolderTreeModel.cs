using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Dtos.Folder
{
    public class FolderTreeModel
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ParentId { get; set; }

        public ICollection<FolderTreeModel>? Children { get; set; } = new List<FolderTreeModel>();
    }
}
