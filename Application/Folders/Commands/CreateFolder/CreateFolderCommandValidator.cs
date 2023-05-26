using FluentValidation;
using FolderManager.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Folders.Commands.CreateFolder
{
    public class CreateFolderCommandValidator : AbstractValidator<CreateFolderCommand>
    {
        private readonly IFolderManagerDbContext _context;
        public CreateFolderCommandValidator(IFolderManagerDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Name must not exceed 90 characters.");

            RuleFor(v => v.ParentId)
                .NotNull().WithMessage("Parent folder id is required");
        }
    }
}
