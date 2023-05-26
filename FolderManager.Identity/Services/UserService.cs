using FolderManager.Application.Common.Interfaces;
using FolderManager.Application.Dtos.User;
using FolderManager.Domain.Entities;
using FolderManager.Identity.Helpers;
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
            new User
            {
                Id = 1,
                Name = "Test",
                Username = "Test",
                Password = "Test123",
            }
        };

        private readonly AuthSettings _authSettings;

        public UserService(IOptions<AuthSettings> appSettings) => _authSettings = appSettings.Value;

        public AuthenticateResponse? Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (user == null)
                return null;

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

        public User GetById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }
    }
}
