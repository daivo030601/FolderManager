using Azure.Identity;
using FolderManager.Application.Common.Interfaces;
using FolderManager.Application.Dtos.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FolderManager.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] AuthenticateRequest request)
        {
            var response = _userService.Authenticate(request);
            if (response == null)
            {
                return BadRequest(new
                {
                    message = "username or password is incorrect"
                });
            }
            return Ok(response);
        }
    }
}
