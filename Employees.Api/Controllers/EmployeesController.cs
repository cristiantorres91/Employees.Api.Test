using Employees.Api.Dtos;
using Employees.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Employees.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeServices _services;

        public EmployeesController(IEmployeeServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            GenericResponse response = new GenericResponse();
            response = await _services.GetEmployees();
            if (response.HttpCode == HttpStatusCode.OK)
                return Ok(response);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeRequest employeeRequest)
        {
            GenericResponse response = new GenericResponse();
            response = await _services.AddEmployee(employeeRequest);
            if (response.HttpCode == HttpStatusCode.OK)
                return Ok(response);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
