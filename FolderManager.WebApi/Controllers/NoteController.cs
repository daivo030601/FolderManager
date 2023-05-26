using FolderManager.Application.Dtos.NoteDto;
using FolderManager.Application.Notes.Commands.CreateNote;
using FolderManager.Application.Notes.Commands.CreateNotes;
using FolderManager.Application.Notes.Commands.DeleteNote;
using FolderManager.Application.Notes.Commands.UpdateNote;
using FolderManager.Application.Notes.Queries.GetNoteById;
using FolderManager.Application.Notes.Queries.GetNotes;
using FolderManager.Application.Notes.Queries.GetSearchNotes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FolderManager.WebApi.Controllers
{
    [ApiController]
    public class NoteController : ApiController
    {
        [HttpGet]
        [Route("Notes/{folderId}")]
        public async Task<ActionResult<GetNotesResponse>> Get(int folderId)
        {
            return await Mediator.Send(new GetNotesQuery{ FolderId = folderId});
        }

        [HttpGet]
        [Route("Notes/{keyword}/{folderId?}")]
        public async Task<ActionResult<GetSearchNotesResponse>> GetSearchNote(string keyword, int folderId)
        {
            return await Mediator.Send(new GetSearchNotesQuery { FolderId = folderId, Keyword = keyword });
        }

        [HttpGet]
        [Route("Note/{id}")]
        public async Task<ActionResult<GetNoteByIdResponse>> GetNoteById(int id)
        {
            return await Mediator.Send(new GetNoteByIdQuery { Id = id });
        }

        [HttpPost]
        [Route("Note")]
        public async Task<ActionResult<int>> Create([FromBody] CreateNoteCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        [Route("Notes")]
        public async Task<ActionResult<List<int>>> CreateMultiNotes([FromBody] ICollection<NoteDto> notes)
        {
            return await Mediator.Send(new CreateNotesCommand { notes = notes});
        }

        [HttpPut]
        [Route("Note")]
        public async Task<ActionResult> Update([FromBody] UpdateNoteCommand command)
        {
            try
            {
                await Mediator.Send(command);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("Note/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteNoteCommand { Id = id });

            return NoContent();
        }
    }
}
