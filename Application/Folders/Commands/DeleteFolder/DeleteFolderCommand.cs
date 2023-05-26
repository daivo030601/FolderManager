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

namespace FolderManager.Application.Folders.Commands.DeleteFolder
{
    public class DeleteFolderCommand : IRequest<Unit> 
    {
        public int Id { get; set; }
    }

    public class DeleteFolderCommandHandler : IRequestHandler<DeleteFolderCommand,Unit>
    {
        private readonly IFolderManagerDbContext _context;
        public DeleteFolderCommandHandler(IFolderManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Folders
                .Where(n => n.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Folder), request.Id);
            }

            _context.Folders.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
