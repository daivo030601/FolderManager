using FolderManager.Application.Common.Exceptions;
using FolderManager.Application.Common.Interfaces;
using FolderManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, Unit>
    {
        private readonly IFolderManagerDbContext _context;
        public DeleteNoteCommandHandler(IFolderManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Notes
                .Where(n => n.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            _context.Notes.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        
    }
}
