using HRManagementSystem.Services.Interfaces;
using HRManagementSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllAsync();

            var model = users.Select(u => new UserViewModel
            {
                UserId = u.UserId,
                Username = u.Username,
                Role = u.Role,
                IsActive = u.IsActive,
            }).ToList();

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

  


    }
}
