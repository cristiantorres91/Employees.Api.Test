using Employees.Api.Dtos;

namespace Employees.Api.Services
{
    public interface IEmployeeServices
    {
        Task<GenericResponse> GetEmployees();
        Task<GenericResponse> AddEmployee(EmployeeRequest employee);
    }
}
