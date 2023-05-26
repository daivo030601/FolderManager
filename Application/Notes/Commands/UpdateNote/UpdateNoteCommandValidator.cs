using FluentValidation;
using FolderManager.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>  
    {
        private readonly IFolderManagerDbContext _context;
        public UpdateNoteCommandValidator(IFolderManagerDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 90 characters.");

            RuleFor(v => v.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(200).WithMessage("Content must not exceed 90 characters.");
        }
    }
}
