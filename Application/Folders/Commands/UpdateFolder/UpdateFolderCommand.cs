using FolderManager.Application.Common.Exceptions;
using FolderManager.Application.Common.Interfaces;
using FolderManager.Application.Dtos.FolderDto;
using FolderManager.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Folders.Commands.UpdateFolder
{
    public class UpdateFolderCommand : IRequest<int>
    {
        public FolderDto Folder { get; set; }
    }

    public class UpdateFolderCommandHandler : IRequestHandler<UpdateFolderCommand, int>
    {
        private readonly IFolderManagerDbContext _context;
        public UpdateFolderCommandHandler(IFolderManagerDbContext context)
        {
            _context = context;    
        }

        public async Task<int> Handle(UpdateFolderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Folders.FindAsync(request.Folder.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Folder), request.Folder.Id);
            }

            entity.Name = request.Folder.Name;

            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
