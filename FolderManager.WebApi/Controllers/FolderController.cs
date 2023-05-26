using FolderManager.Application.Folders.Commands.CreateFolder;
using FolderManager.Application.Folders.Commands.DeleteFolder;
using FolderManager.Application.Folders.Commands.UpdateFolder;
using FolderManager.Application.Folders.Queries.GetFolderTree;
using FolderManager.Application.Notes.Commands.CreateNote;
using FolderManager.Application.Notes.Commands.DeleteNote;
using FolderManager.Application.Notes.Commands.UpdateNote;
using FolderManager.Application.Notes.Queries.GetNotes;
using Microsoft.AspNetCore.Mvc;

namespace FolderManager.WebApi.Controllers
{
    [ApiController]
    public class FolderController : ApiController
    {
        [HttpGet]
        [Route("Folders")]
        public async Task<ActionResult<GetFolderTreeResponse>> Get()
        {
            return await Mediator.Send(new GetFolderTreeQuery());
        }

        [HttpPost]
        [Route("Folder")]
        public async Task<ActionResult<int>> Create([FromBody] CreateFolderCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        [Route("Folder")]
        public async Task<ActionResult<int>> Update([FromBody] UpdateFolderCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        [Route("Folder/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteFolderCommand { Id = id });

            return NoContent();
        }
    }
}
