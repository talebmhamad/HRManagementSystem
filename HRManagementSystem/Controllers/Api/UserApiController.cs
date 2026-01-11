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

        [HttpPost("deactivate/{employeeId}")]
        public async Task<IActionResult> DeactivateUser(int employeeId)
        {
            var result = await _userService.DeactivateUser(employeeId);

            if (!result)
            {
                return NotFound("User not found");
            }

            return Ok("User deactivated successfully");
        }
    }
}
