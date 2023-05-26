using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Folders.Commands.UpdateFolder
{
    public class UpdateFolderCommandValidator : AbstractValidator<UpdateFolderCommand>
    {
        public UpdateFolderCommandValidator()
        {
            RuleFor(v => v.Folder.Name)
                .NotEmpty().WithMessage("update name is required");
        }
    }
}
