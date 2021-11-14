using AutoMapper;
using Employees.Api.Dtos;
using Employees.Api.Models;
using Employees.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Employees.Api.Test
{
    public class EmployeeServicesTest
    {
        private Employee employee;
        private EmployeeRequest employeeRequest;
        private  IMapper mapper;
        [SetUp]
        public void Setup()
        {
            mapper = Substitute.For<IMapper>();
            employee = new Employee()
            {
                Id = 1,
                Name = "Cristian",
                Age = 30
            };
            employeeRequest = new EmployeeRequest()
            {
                Name = "Cristian",
                Age = 30
            };

        }

        private IServiceProvider CreateContext(string nameDB)
        {
            var services = new ServiceCollection();

            services.AddDbContext<EmployeeContext>(opt => opt.UseInMemoryDatabase(databaseName: nameDB),
                ServiceLifetime.Scoped,
                ServiceLifetime.Scoped);

            return services.BuildServiceProvider();
        }

        [TestCase(HttpStatusCode.OK)]
        [TestCase(HttpStatusCode.InternalServerError)]
        public async Task When_Add_Employee_Services(HttpStatusCode code)
        {
            //Arrange
            var nameDB = Guid.NewGuid().ToString();
            var serviceProvider = CreateContext(nameDB);

            var db = serviceProvider.GetService<EmployeeContext>();
            db.Add(employee);

            //Act
            if (code == HttpStatusCode.OK)
                mapper.Map<Employee>(employeeRequest).ReturnsForAnyArgs(employee);
            else
                mapper.Map<Employee>(employeeRequest).ThrowsForAnyArgs(x => { throw new Exception(); });

            EmployeeServices services = new EmployeeServices(db, mapper);
            var responseServices = await services.AddEmployee(employeeRequest);

            //Assert
            Assert.AreEqual(code, (responseServices.HttpCode));
        }
    }
}