using HRManagementSystem.Data.DTOs;
using System.Security.Claims;


namespace HRManagementSystem.Services.Interfaces
{
    public interface IClaimsService
    {
        ClaimsPrincipal CreateClaims(UserDto user);
    }
}
