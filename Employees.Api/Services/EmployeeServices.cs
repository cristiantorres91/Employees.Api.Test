using AutoMapper;
using Employees.Api.Dtos;
using Employees.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Employees.Api.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly EmployeeContext _context;
        private readonly IMapper _mapper;

        public EmployeeServices(EmployeeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponse> GetEmployees()
        {
            GenericResponse response;
            try
            {
                var employees = await _context.Employees.ToListAsync();
                response = new GenericResponse()
                {
                    Data = employees,
                    HttpCode = HttpStatusCode.OK,
                    Message = "Listado Empleados"
                };
            }
            catch (Exception e)
            {

                response = new GenericResponse()
                {
                    HttpCode = HttpStatusCode.InternalServerError,
                    Message = $"Error al insertar registro: {e.Message}"
                };
            }

            return response;
        }
        public async Task<GenericResponse> AddEmployee(EmployeeRequest employeeRequest)
        {
            GenericResponse response;
            try
            {
                var employee = _mapper.Map<Employee>(employeeRequest);
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                response = new GenericResponse()
                {
                    HttpCode = HttpStatusCode.OK,
                    Message = "Empleado Agredado",
                };
            }
            catch (Exception e)
            {
                response = new GenericResponse()
                {
                    HttpCode = HttpStatusCode.InternalServerError,
                    Message = $"Error al insertar registro: {e.Message}"
                };
            }

            return response;
        }

    }
}
