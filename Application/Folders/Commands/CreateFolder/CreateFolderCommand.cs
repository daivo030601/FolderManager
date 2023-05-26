using FolderManager.Application.Common.Interfaces;
using FolderManager.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Folders.Commands.CreateFolder
{
    public class CreateFolderCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int ParentId { get; set; }   
    }

    public class CreateFolderCommandHandler : IRequestHandler<CreateFolderCommand, int>
    {
        private readonly IFolderManagerDbContext _context;

        public CreateFolderCommandHandler(IFolderManagerDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
        {
            var entity = new Folder
            {
                Name = request.Name,
                ParentId = request.ParentId
            };

            _context.Folders.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
