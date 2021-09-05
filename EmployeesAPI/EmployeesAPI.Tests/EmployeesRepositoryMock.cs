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
    public class EmployeesRepositoryMock
    {
        private readonly EmployeesList EmployeesList;
        public Mock<IEmployeesRepository> Repository { get; }

        public EmployeesRepositoryMock()
        {
            EmployeesList = new EmployeesList();
            List<Employee> employeesList = new List<Employee>();

            Repository = new Mock<IEmployeesRepository>();
            //   Repository.Setup(p => p.GetAsync(new Guid())).ReturnsAsync(new Response<Employee>(content: null));
            //     Repository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => new Response<Employee>(employeesList.Find(e => e.Id.Equals(id))));
            Repository.Setup(x => x.AddAsync(It.IsAny<Employee>())).ReturnsAsync((Employee emp) => AddEmployee(emp));
            Repository.Setup(x => x.GetByParameterAsync(e => e.Role == RoleTypes.CEO && !e.Id.Equals(new Guid()))).ReturnsAsync(new Response<IEnumerable<Employee>>(employeesList.FindAll(e => e.Id.Equals(new Guid()))));

        }


        public Response<Employee> AddEmployee(Employee emp)
        {
            emp.Id = Guid.NewGuid();
            EmployeesList.Employees.Add(emp);
            return new Response<Employee>(emp);
        }
        /*
        public Response<Employee> AddAsync(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            EmployeesList.Employees.Add(employee);
            return new Response<Employee>(employee);
        }

        public void DeleteAsync(Guid id)
        {
            var existing = EmployeesList.Employees.First(a => a.Id == id);
            EmployeesList.Employees.Remove(existing);
        }*/
    }
}
