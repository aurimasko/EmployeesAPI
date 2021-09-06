using System;
using System.Threading.Tasks;
using EmployeesAPI.Domain.Common;
using EmployeesAPI.Domain.Configuration;
using EmployeesAPI.Domain.Models;
using EmployeesAPI.Domain.Services;
using Xunit;

namespace EmployeesAPI.Tests
{
    public class EmployeesControlller_DeleteTests
    {
        readonly EmployeesService _service;

        public EmployeesControlller_DeleteTests()
        {
            EmployeesRepositoryMock _repository = new EmployeesRepositoryMock();
            _service = new EmployeesService(_repository.Repository.Object);
        }

        [Fact]
        public void EmployeesController_DeleteEmployee_RemoveBossFailed_RemoveSubordinatesFirst()
        {
            // Arrange
            Guid employeeId = new Guid("57b06b90-a401-4505-bcfb-1fb9e8735bae");

            // Act
            var returned = _service.DeleteAsync(employeeId);

            // Assert
            Assert.IsType<Task<Response<Employee>>>(returned);
            Assert.Contains("This employee has at least one subordinate! Change boss for subordinatess first.", returned.Result.ErrorMessages);
        }

        [Fact]
        public void EmployeesController_DeleteEmployee_RemoveEmployee_Success()
        {
            // Arrange
            Guid employeeId = new Guid("79d01eaa-c59a-4483-917f-613c5a3ba20c");

            // Act
            var returned = _service.DeleteAsync(employeeId);

            // Assert
            Assert.IsType<Task<Response<Employee>>>(returned);
            Assert.Equal(employeeId, returned.Result.Content.Id);
            Assert.Empty(returned.Result.ErrorMessages);
        }
    }
}
