using FolderManager.Application.Dtos.User;
using FolderManager.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model);
        Task<IdentityResult> SignUpAsync(SignUpRequest model);
        Task<User> GetById(string id);
    }
}
