using FluentValidation;
using FolderManager.Application.Common.Interfaces;
using FolderManager.Application.Notes.Commands.CreateNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Notes.Commands.CreateNotes
{
    public class CreateNotesCommandValidator : AbstractValidator<CreateNotesCommand>
    {
        private readonly IFolderManagerDbContext _context;
        public CreateNotesCommandValidator(IFolderManagerDbContext context)
        {
            _context = context;
            RuleFor(v => v.notes)
                .NotEmpty().WithMessage("The list notes to created cannot be empty");
        }
    }
}
