using FluentValidation;
using FolderManager.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Notes.Queries.GetSearchNotes
{
    public class GetSearchNotesQueryValidator : AbstractValidator<GetSearchNotesQuery>
    {
        private readonly IFolderManagerDbContext _context;

        public GetSearchNotesQueryValidator(IFolderManagerDbContext context)
        {
            _context = context;

            RuleFor(v => v.FolderId)
                .NotEmpty().WithMessage("Folder Id is required");
        }
    }
}
