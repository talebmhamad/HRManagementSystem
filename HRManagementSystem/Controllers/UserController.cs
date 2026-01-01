using HRManagementSystem.Services.Interfaces;
using HRManagementSystem.Web.Mappers;
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
            var users = await _userService.GetAll();

            var model = users.Select(UserModelMapper.ToViewModel).ToList();

            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var userDto = await _userService.GetById(id);
            if (userDto == null)
            {
                return NotFound();
            }

            var model = UserModelMapper.ToEditViewModel(userDto);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = UserModelMapper.ToDto(model);
            var result = await _userService.UpdateUser(dto);

            if (result == null)
            {
                ModelState.AddModelError("", "Failed to update user.");
                return View(model);
            }

            TempData["SuccessMessage"] = "User updated successfully.";
            return RedirectToAction(nameof(Index));
        }




    }
}
