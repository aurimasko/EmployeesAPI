using System;
using EmployeesAPI.API.Controllers;
using EmployeesAPI.Domain.Interfaces;
using Xunit;
using Moq;
using EmployeesAPI.Domain.Common;
using EmployeesAPI.Domain.Models;
using EmployeesAPI.API;
using EmployeesAPI.Domain.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesAPI.Tests
{
    public class EmployeesController_GetByIdTests
    {
        readonly EmployeesController _controller;

        public EmployeesController_GetByIdTests()
        {
          
            EmployeesServiceMock _service = new EmployeesServiceMock();
            _controller = new EmployeesController(_service.Service.Object);
        }

        [Fact]
        public void EmployeesController_GetById_Returns_OkObjectResult()
        {
            // Act
            var result = _controller.Get(new Guid("57b06b90-a401-4505-bcfb-1fb9e8735bae"));

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void EmployeesController_GetById_Returns_CorrectResult()
        {
            // Arrange
            Guid employeeId = new Guid("57b06b90-a401-4505-bcfb-1fb9e8735bae");

            // Act
            var result = _controller.Get(employeeId).Result as OkObjectResult;

            // Assert
            var employee = Assert.IsType<Response<Employee>>(result.Value);
            Assert.NotNull(employee.Content);
            Assert.Equal(employee.Content.Id, employeeId);
        }

        [Fact]
        public void EmployeesController_GetById_Returns_ZeroResult()
        {
            // Arrange
            Guid employeeId = new Guid();

            // Act
            var result = _controller.Get(employeeId).Result as OkObjectResult;

            // Assert
            var employee = Assert.IsType<Response<Employee>>(result.Value);
            Assert.Null(employee.Content);
        }
    }
}
