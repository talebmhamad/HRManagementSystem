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

        // GET: UserController
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllAsync();

            var model = users.Select(u => new UserViewModel
            {
                UserId = u.UserId,
                Username = u.Username,
                Role = u.Role,
                IsActive = u.IsActive,
                EmployeeName = u.Employee.FirstName + " " + u.Employee.LastName
            }).ToList();

            return View(model);
        }


        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

  


    }
}
