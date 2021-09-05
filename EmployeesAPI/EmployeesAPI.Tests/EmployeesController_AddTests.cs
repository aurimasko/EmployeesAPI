using System;
using System.Threading.Tasks;
using AutoMapper;
using EmployeesAPI.API;
using EmployeesAPI.API.Controllers;
using EmployeesAPI.API.DTO;
using EmployeesAPI.Domain.Common;
using EmployeesAPI.Domain.Configuration;
using EmployeesAPI.Domain.Models;
using EmployeesAPI.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace EmployeesAPI.Tests
{
    public class EmployeesController_AddTests
    {
        readonly EmployeesService _service;
        readonly IMapper _mapper;

        public EmployeesController_AddTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            EmployeesRepositoryMock _repository = new EmployeesRepositoryMock();

            _service = new EmployeesService(_repository.Repository.Object);
        }

        [Fact]
        public void EmployeesController_AddEmployee_Returns_Success()
        {
            // Arrange
            Employee employee = new Employee()
            {
                FirstName = "Edvard",
                LastName = "Gome",
                BirthDate = new DateTime(2000, 01, 01),
                EmploymentDate = new DateTime(2010, 01, 01),
                Salary = 7000,
                BossId = new Guid("57b06b90-a401-4505-bcfb-1fb9e8735bae"),
                Role = RoleTypes.Employee,
                HomeAddress = "Address 2"
            };
            // Act
            var result = _service.AddAsync(employee);

            // Assert
            Assert.IsType<Response<Employee>>(result.Result);
        }

        [Fact]
        public void EmployeesController_AddEmployee_Returns_BossIdMissing()
        {
            // Arrange
            Employee employee = new Employee()
            {
                FirstName = "Edvard",
                LastName = "Gome",
                BirthDate = new DateTime(2000, 01, 01),
                EmploymentDate = new DateTime(2010, 01, 01),
                Salary = 7000,
                Role = RoleTypes.Employee,
                HomeAddress = "Address 2"
            };
            // Act
            var returned = _service.AddAsync(employee);

            // Assert
            Assert.IsType<Task<Response<Employee>>>(returned);
            Assert.Contains("Boss id must be filled.", returned.Result.ErrorMessages);
        }

        [Fact]
        public void EmployeesController_AddEmployee_Returns_WrongRole()
        {
            // Arrange
            Employee employee = new Employee()
            {
                FirstName = "Edvard",
                LastName = "Gome",
                BirthDate = new DateTime(2000, 01, 01),
                EmploymentDate = new DateTime(2010, 01, 01),
                Salary = 7000,
                Role = RoleTypes.CEO,
                BossId = new Guid("57b06b90-a401-4505-bcfb-1fb9e8735bae"),
                HomeAddress = "Address 2"
            };
            // Act
            var returned = _service.AddAsync(employee);

            // Assert
            Assert.IsType<Task<Response<Employee>>>(returned);
            Assert.Contains("Only one employee can have CEO role.", returned.Result.ErrorMessages);
        }

        [Fact]
        public void EmployeesController_AddEmployee_Returns_TooYoung()
        {
            // Arrange
            Employee employee = new Employee()
            {
                FirstName = "Edvard",
                LastName = "Gome",
                BirthDate = new DateTime(2010, 01, 01),
                EmploymentDate = new DateTime(2010, 01, 01),
                Salary = 7000,
                Role = RoleTypes.Employee,
                BossId = new Guid("57b06b90-a401-4505-bcfb-1fb9e8735bae"),
                HomeAddress = "Address 2"
            };
            // Act
            var returned = _service.AddAsync(employee);

            // Assert
            Assert.IsType<Task<Response<Employee>>>(returned);
            Assert.Contains("Employee must be at least 18 years old.", returned.Result.ErrorMessages);
        }

        [Fact]
        public void EmployeesController_AddEmployee_Returns_TooOld()
        {
            // Arrange
            Employee employee = new Employee()
            {
                FirstName = "Edvard",
                LastName = "Gome",
                BirthDate = new DateTime(1950, 09, 04),
                EmploymentDate = new DateTime(2010, 01, 01),
                Salary = 7000,
                Role = RoleTypes.Employee,
                BossId = new Guid("57b06b90-a401-4505-bcfb-1fb9e8735bae"),
                HomeAddress = "Address 2"
            };
            // Act
            var returned = _service.AddAsync(employee);

            // Assert
            Assert.IsType<Task<Response<Employee>>>(returned);
            Assert.Contains("Employee must be not older than 70 years old.", returned.Result.ErrorMessages);
        }

        [Fact]
        public void EmployeesController_AddEmployee_Returns_Success2()
        {
            // Arrange
            Employee employee = new Employee()
            {
                FirstName = "Edvard",
                LastName = "Gome",
                BirthDate = new DateTime(1950, 09, 06),
                EmploymentDate = new DateTime(2010, 01, 01),
                Salary = 7000,
                Role = RoleTypes.Employee,
                BossId = new Guid("57b06b90-a401-4505-bcfb-1fb9e8735bae"),
                HomeAddress = "Address 2"
            };
            // Act
            var returned = _service.AddAsync(employee);

            // Assert
            Assert.IsType<Task<Response<Employee>>>(returned);
            Assert.Empty(returned.Result.ErrorMessages);
            Assert.True(returned.Result.IsSuccess);
        }

        [Fact]
        public void EmployeesController_AddEmployee_Returns_WrongSalary()
        {
            // Arrange
            Employee employee = new Employee()
            {
                FirstName = "Edvard",
                LastName = "Gome",
                BirthDate = new DateTime(1970, 09, 06),
                EmploymentDate = new DateTime(2010, 01, 01),
                Salary = -1,
                Role = RoleTypes.Employee,
                BossId = new Guid("57b06b90-a401-4505-bcfb-1fb9e8735bae"),
                HomeAddress = "Address 2"
            };
            // Act
            var returned = _service.AddAsync(employee);

            // Assert
            Assert.IsType<Task<Response<Employee>>>(returned);
            Assert.Contains("Current salary must be bigger than 0.", returned.Result.ErrorMessages);
        }
    }
}
