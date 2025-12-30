using HRManagementSystem.Data.DTOs;
using HRManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;



namespace HRManagementSystem.Services.Implementations
{
    public class ClaimsService : IClaimsService
    {
        public ClaimsPrincipal CreateClaims(UserDto user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(identity);
        }
    }
}
