using HRManagementSystem.Services.Interfaces;
using HRManagementSystem.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IClaimsService claimsService;
        public AuthController(IAuthService authService, IClaimsService claimsService)
        {
            this._authService = authService;
            this.claimsService = claimsService; 
        }

        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel request, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", request);
            }

            var user = await _authService.AuthenticateAsync(request.Username, request.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password");
                return View("Index", request);
            }

            await HttpContext.SignInAsync(claimsService.CreateClaims(user));

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Auth");
        }



    }
}
