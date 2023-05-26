using FolderManager.Application.Common.Interfaces;
using FolderManager.Application.Dtos.NoteDto;
using FolderManager.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Notes.Commands.CreateNotes
{
    public class CreateNotesCommand : IRequest<List<int>>
    {
        public ICollection<NoteDto> notes;
    }

    public class CreateNotesCommandHandler : IRequestHandler<CreateNotesCommand, List<int>>
    {
        private readonly IFolderManagerDbContext _context;
        public CreateNotesCommandHandler(IFolderManagerDbContext context)
        {
            _context = context;
        }
        public async Task<List<int>> Handle(CreateNotesCommand request, CancellationToken cancellationToken)
        {
            List<int> response = new List<int>();
            foreach (var note in request.notes)
            {
                Note entity = new Note
                {
                    Title = note.Title,
                    Content = note.Content,
                    FolderId = note.FolderId,
                };
                _context.Notes.Add(entity);
                response.Add(entity.Id);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return response;
        }
    }
}
