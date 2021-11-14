using Employees.Api.Controllers;
using Employees.Api.Dtos;
using Employees.Api.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Api.Test
{
    public class  EmployeesControllerTest
    {
        private IEmployeeServices employeeServices;
        private EmployeeRequest employeeRequest;

        [SetUp]
        public void SetUp()
        {
            employeeServices = Substitute.For<IEmployeeServices>();
            employeeRequest = new EmployeeRequest()
            {
                Name = "Cristian",
                Age = 30
            };
        }

        [TestCase(HttpStatusCode.OK)]
        [TestCase(HttpStatusCode.InternalServerError)]
        public async Task When_Add_Employee_Controller(HttpStatusCode code)
        {
            //Arrange
            GenericResponse response = new GenericResponse()
            {
                HttpCode = code
            };

            //Act
            employeeServices.AddEmployee(employeeRequest).ReturnsForAnyArgs(response);
            EmployeesController controller = new EmployeesController(employeeServices);
            ObjectResult responseController = (ObjectResult)await controller.AddEmployee(employeeRequest);

            //Assert
            Assert.AreEqual((int)code, responseController.StatusCode.Value);
        }
    }
}
