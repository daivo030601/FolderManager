using FolderManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Common.Interfaces
{
    public interface IFolderManagerDbContext
    {
        DbSet<Note> Notes { get; set; }
        DbSet<Folder> Folders { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
