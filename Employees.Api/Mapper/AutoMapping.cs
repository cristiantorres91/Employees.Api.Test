using AutoMapper;
using Employees.Api.Dtos;
using Employees.Api.Models;

namespace Employees.Api.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<EmployeeRequest, Employee>();
        }
    }
}
