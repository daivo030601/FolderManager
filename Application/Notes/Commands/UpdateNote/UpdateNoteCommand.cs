using FolderManager.Application.Common.Exceptions;
using FolderManager.Application.Common.Interfaces;
using FolderManager.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
       
    }

    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Unit>
    {
        private readonly IFolderManagerDbContext _context;

        public UpdateNoteCommandHandler(IFolderManagerDbContext context)
        {
            
            _context = context;
        }

        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Notes.FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            entity.Title = request.Title;
            entity.Content = request.Content;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
