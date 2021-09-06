using System;
using System.Collections.Generic;
using System.Linq;
using EmployeesAPI.API;
using EmployeesAPI.API.Controllers;
using EmployeesAPI.Domain.Common;
using EmployeesAPI.Domain.Models;
using EmployeesAPI.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EmployeesAPI.Tests
{
    public class EmployeesController_GetSearchTests
    {
         EmployeesController _controller;

         public EmployeesController_GetSearchTests()
         {
             EmployeesRepositoryMock _repository = new EmployeesRepositoryMock();

             _controller = new EmployeesController(new EmployeesService(_repository.Repository.Object));
         }

         [Fact]
         public void EmployeesController_GetSearch_Returns_OkObjectResult()
         {
             // Act
             var result = _controller.Get(name: "John", null, null);

             // Assert
             Assert.IsType<OkObjectResult>(result.Result);
         }

         [Fact]
         public void EmployeesController_GetSearch_Returns_OneResult_BirthdateIntervalFilled()
         {
             // Act
             var result = _controller.Get(name: "John", new DateTime(1980, 01, 01), new DateTime(2010, 01, 01)).Result as OkObjectResult;

             // Assert
             var employee = Assert.IsType<Response<IEnumerable<Employee>>>(result.Value);
             Assert.NotNull(employee.Content);
             Assert.Equal(2, employee.Content.Count());
         }

         [Fact]
         public void EmployeesController_GetSearch_Returns_OneResult_BirthdateIntervalFilled2()
         {
             // Act
             var result = _controller.Get(name: "John", new DateTime(1992, 01, 01), new DateTime(1992, 05, 01)).Result as OkObjectResult;

             // Assert
             var employee = Assert.IsType<Response<IEnumerable<Employee>>>(result.Value);
             Assert.NotNull(employee.Content);
             Assert.Single(employee.Content);
         }


         [Fact]
         public void EmployeesController_GetSearch_Returns_OneResult_BirthdateToFilled()
         {
             // Act
             var result = _controller.Get(name: "John", null, new DateTime(1992, 05, 01)).Result as OkObjectResult;

             // Assert
             var employee = Assert.IsType<Response<IEnumerable<Employee>>>(result.Value);
             Assert.NotNull(employee.Content);
             Assert.Single(employee.Content);
         }

         [Fact]
         public void EmployeesController_GetSearch_Returns_OneResult_BirthdateFromFilled()
         {
             // Act
             var result = _controller.Get(name: "John", new DateTime(1992, 05, 01), null).Result as OkObjectResult;

             // Assert
             var employee = Assert.IsType<Response<IEnumerable<Employee>>>(result.Value);
             Assert.NotNull(employee.Content);
             Assert.Equal(2, employee.Content.Count());
         }

         [Fact]
         public void EmployeesController_GetSearch_Returns_OneResult_BirthdateIntervalNotFilled()
         {
             // Act
             var result = _controller.Get(name: "John", null, null).Result as OkObjectResult;

             // Assert
             var employee = Assert.IsType<Response<IEnumerable<Employee>>>(result.Value);
             Assert.NotNull(employee.Content);
             Assert.Equal(2, employee.Content.Count());
         }
     }
}
