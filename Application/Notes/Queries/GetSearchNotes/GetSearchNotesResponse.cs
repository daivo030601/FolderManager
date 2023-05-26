
using FolderManager.Application.Dtos.NoteDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Notes.Queries.GetSearchNotes
{
    public class GetSearchNotesResponse
    {
        public IList<NoteDto> ResponseData { get; set; } = new List<NoteDto>();
    }
}
