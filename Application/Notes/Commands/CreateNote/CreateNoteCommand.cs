using FolderManager.Application.Common.Interfaces;
using FolderManager.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Notes.Commands.CreateNote
{
    public partial class CreateNoteCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int? FolderId { get; set; }
    }

    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, int>
    {
        private readonly IFolderManagerDbContext _context;
        public CreateNoteCommandHandler(IFolderManagerDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = new Note 
            { 
                Content = request.Content,
                FolderId = request.FolderId,
                Title = request.Title,
            };

            _context.Notes.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
