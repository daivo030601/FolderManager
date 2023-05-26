using FolderManager.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FolderManager.Domain.Entities;

namespace FolderManager.Application.Dtos.FolderDto
{
    public class FolderDto : IMapFrom<FolderManager.Domain.Entities.Folder>
    {
        public int? Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
