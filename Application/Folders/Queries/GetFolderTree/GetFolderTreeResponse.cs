
using FolderManager.Application.Dtos.Folder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Folders.Queries.GetFolderTree
{
    public class GetFolderTreeResponse
    {
        public IList<FolderTreeModel> Tree { get; set; }
    }
}
