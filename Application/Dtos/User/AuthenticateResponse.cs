using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Dtos.User
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(Domain.Entities.User user, string token)
        {
            Id = user.Id;
            Token = token;
        }
    }
}
