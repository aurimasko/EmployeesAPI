using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EmployeesAPI.Domain.Common;
using EmployeesAPI.Domain.Configuration;
using EmployeesAPI.Domain.Interfaces;
using EmployeesAPI.Domain.Models;
using Moq;

namespace EmployeesAPI.Tests
{
    public class EmployeesRepositoryMock
    {
        private readonly EmployeesList EmployeesList;
        public Mock<IEmployeesRepository> Repository { get; }

        public EmployeesRepositoryMock()
        {
            EmployeesList = new EmployeesList();

            Repository = new Mock<IEmployeesRepository>();
            Repository.Setup(x => x.AddAsync(It.IsAny<Employee>())).ReturnsAsync((Employee emp) => AddEmployee(emp));
            Repository.Setup(x => x.DeleteAsync(It.IsAny<Employee>())).ReturnsAsync((Employee emp) => DeleteEmployee(emp));

            Repository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => new Response<Employee>(EmployeesList.Employees.Find(e => e.Id.Equals(id))));

            Repository.Setup(x => x.GetByParameterAsync(It.IsAny<Expression<Func<Employee, bool>>>())).ReturnsAsync((Expression<Func<Employee, bool>> criteria) => new Response<IEnumerable<Employee>>(EmployeesList.Employees.Where(criteria.Compile()).ToList()));
        }

        public Response<Employee> AddEmployee(Employee emp)
        {
            emp.Id = Guid.NewGuid();
            EmployeesList.Employees.Add(emp);
            return new Response<Employee>(emp);
        }

        public Response<Employee> DeleteEmployee(Employee emp)
        {
            EmployeesList.Employees.Remove(emp);
            return new Response<Employee>(emp);
        }
    }
}
