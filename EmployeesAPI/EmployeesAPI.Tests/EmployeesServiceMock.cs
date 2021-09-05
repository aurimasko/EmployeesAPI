using System;
using System.Collections.Generic;
using System.Linq;
using EmployeesAPI.Domain.Common;
using EmployeesAPI.Domain.Configuration;
using EmployeesAPI.Domain.Interfaces;
using EmployeesAPI.Domain.Models;
using Moq;

namespace EmployeesAPI.Tests
{
    public class EmployeesServiceMock
    {
        public Mock<IEmployeesService> Service { get; }
        
        public EmployeesServiceMock()
        {
            List<Employee> employeesList = new EmployeesList().Employees;

            Service = new Mock<IEmployeesService>();
            Service.Setup(p => p.GetAsync(new Guid())).ReturnsAsync(new Response<Employee>(content: null));
            Service.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => new Response<Employee>(employeesList.Find(e => e.Id.Equals(id))));
        }
    }
}
