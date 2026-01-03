using HRManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Web.Controllers.Api
{
    [Route("api/User")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserApiController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("deactivate/{userId}")]
        public async Task<IActionResult> DeactivateUser(int userId)
        {
            var result = await _userService.DeactivateUser(userId);

            if (!result)
            {
                return NotFound("User not found");
            }

            return Ok("User deactivated successfully");
        }
    }
}
