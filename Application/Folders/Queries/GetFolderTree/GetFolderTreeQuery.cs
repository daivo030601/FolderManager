using FolderManager.Application.Common.Interfaces;
using FolderManager.Application.Dtos.Folder;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Folders.Queries.GetFolderTree
{
    public class GetFolderTreeQuery : IRequest<GetFolderTreeResponse> { }

    public class GetFolderTreeQueryHandler : IRequestHandler<GetFolderTreeQuery, GetFolderTreeResponse>
    {
        private readonly IFolderManagerDbContext _context;
        public GetFolderTreeQueryHandler(IFolderManagerDbContext context)
        {
            _context = context;
        }

        public async Task<GetFolderTreeResponse> Handle(GetFolderTreeQuery request, CancellationToken cancellationToken)
        {
            ICollection<FolderTreeModel> folders = await GetAllFolders();
            foreach (var folder in folders)
            {
                folder.Children = await GetFolderChildren(folders, folder);
            }
            return new GetFolderTreeResponse
            {
                Tree = folders.Where(f => f.ParentId == 0).ToList()
            };
        }

        public async Task<ICollection<FolderTreeModel>> GetAllFolders()
        {
            ICollection<FolderTreeModel> response = new List<FolderTreeModel>();
            var dataList = await _context.Folders
                .ToListAsync();
            dataList.ForEach(row => response.Add(new FolderTreeModel()
            {
                Id = row.Id,
                Name = row.Name,
                ParentId = row.ParentId,
            }));
            return response;
        }

        public async Task<ICollection<FolderTreeModel>> GetFolderChildren(ICollection<FolderTreeModel> allFolders, FolderTreeModel folder)
        {
            if (allFolders.All(f => f.ParentId != folder.Id)) return new List<FolderTreeModel>();

            folder.Children = allFolders
                .Where(b => b.ParentId == folder.Id)
                .ToList();
            foreach (var item in folder.Children)
            {
                item.Children = await GetFolderChildren(allFolders, item);
            }

            return folder.Children;
        }
    }
    
}
