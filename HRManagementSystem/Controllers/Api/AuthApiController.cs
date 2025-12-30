using HRManagementSystem.Services.Interfaces;
using HRManagementSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Web.Controllers.Api
{
    [ApiController]
    [Route("api/auth")]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthApiController(IAuthService authService){
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel request)
        {
            var user = await _authService.AuthenticateAsync(request.Username,request.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            return Ok(new{user.UserId,user.Username,user.Role});
        }
    }
}
