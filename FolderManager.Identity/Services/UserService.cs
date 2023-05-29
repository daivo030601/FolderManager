using FolderManager.Application.Common.Interfaces;
using FolderManager.Application.Dtos.User;
using FolderManager.Domain.Entities;
using FolderManager.Identity.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new List<User>
        {
            
        };
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AuthSettings _authSettings;

        public UserService(UserManager<User> userManager,
            SignInManager<User> signInManager, IOptions<AuthSettings> appSettings) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authSettings = appSettings.Value;
        }

        public async Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }

            var user = await _userManager.FindByNameAsync(model.Username);
            var token = GenerateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        private string GenerateJwtToken(User user)
        {
            byte[] key = Encoding.ASCII.GetBytes(_authSettings.Secret);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<User> GetById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> SignUpAsync(SignUpRequest model)
        {
            try
            {

                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName
                };
                return await _userManager.CreateAsync(user, model.Password);
            } catch(Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
