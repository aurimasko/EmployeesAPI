using System;
using System.Collections.Generic;
using EmployeesAPI.Domain.Configuration;
using EmployeesAPI.Domain.Models;

namespace EmployeesAPI.Tests
{
    public class EmployeesList
    {
        public List<Employee> Employees { get; }

        public EmployeesList()
        {
            Employees = new List<Employee> {
                new Employee
                {
                    Id = new Guid("57b06b90-a401-4505-bcfb-1fb9e8735bae"),
                    FirstName = "John",
                    LastName = "Johny",
                    BirthDate = new DateTime(1992, 05, 01),

                    EmploymentDate = new DateTime(2021, 09, 01),
                    HomeAddress = "Address street",
                    Role = RoleTypes.CEO,
                    Salary = 5000
                },

                new Employee
                {
                    Id = new Guid("79d01eaa-c59a-4483-917f-613c5a3ba20c"),
                    FirstName = "Jay",
                    LastName = "Hals",
                    BirthDate = new DateTime(1990, 05, 01),
                    EmploymentDate = new DateTime(2021, 09, 02),
                    HomeAddress = "Address street2",
                    Role = RoleTypes.Employee,
                    Salary = 2000,
                    BossId = new Guid("57b06b90-a401-4505-bcfb-1fb9e8735bae")
                },
                new Employee
                {
                    Id = new Guid("d1899425-851b-4580-abcf-adc59345ba07"),
                    FirstName = "John",
                    LastName = "Hals",
                    BirthDate = new DateTime(1995, 05, 01),
                    EmploymentDate = new DateTime(2021, 09, 02),
                    HomeAddress = "Address street2",
                    Role = RoleTypes.Employee,
                    Salary = 2000,
                    BossId = new Guid("57b06b90-a401-4505-bcfb-1fb9e8735bae")
                }

            };
        }
    }
}
