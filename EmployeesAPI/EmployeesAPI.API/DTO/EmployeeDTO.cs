using System;
using System.ComponentModel.DataAnnotations;
using EmployeesAPI.Domain.Configuration;

namespace EmployeesAPI.API.DTO
{
    public class EmployeeDTO
    {
        public EmployeeDTO()
        {
        }

        public Guid Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime EmploymentDate { get; set; }
        public String HomeAddress { get; set; }
        public int Salary { get; set; }
        public RoleTypes Role { get; set; }
        public Guid? BossId { get; set; }
        public byte[] RowVersion { get; set; }
    }
}


