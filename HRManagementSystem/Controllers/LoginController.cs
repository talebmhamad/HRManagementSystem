using HRManagementSystem.Services.Interfaces;
using HRManagementSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            //if (!ModelState.IsValid)
            //    return View("Index", request);

            //var user = await _userService.AuthenticateAsync(request.Email, request.Password);

            //if (user == null)
            //{
            //    ModelState.AddModelError("", "Invalid username or password");
            //    return View("Index", request);
            //}

            return RedirectToAction("Index", "Home");
        }



    }
}
