using HRManagementSystem.Services.Interfaces;
using HRManagementSystem.Web.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Web.Controllers.Api
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeApiController(IEmployeeService employeeService) 
        {
            _employeeService = employeeService;
        }

        [HttpGet("profile/{employeeId}")]
        public async Task<IActionResult>  Get(int employeeId) 
        {
            var entity = await _employeeService.GetEmployeeById(employeeId);
            if (entity == null)
            {
                return NotFound("employee not found");
            }
            var employee = EmployeeModelMapper.ToViewModel(entity);

            return Ok(employee);

        }

      
    }
}
