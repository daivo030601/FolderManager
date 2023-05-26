using FolderManager.Application.Common.Mappings;
using FolderManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Notes.Queries.GetNoteById
{
    public class GetNoteByIdResponse : IMapFrom<Note>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? FolderId { get; set; }
    }
}
