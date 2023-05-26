using FluentValidation;
using FolderManager.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Notes.Queries.GetNoteById
{
    public class GetNoteByIdQueryValidator : AbstractValidator<GetNoteByIdQuery>
    {
        private readonly IFolderManagerDbContext _context;

        public GetNoteByIdQueryValidator(IFolderManagerDbContext context)
        {
            _context = context;

            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Id is required");
        }
    }
}
