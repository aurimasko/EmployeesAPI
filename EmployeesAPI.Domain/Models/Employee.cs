using System;
using System.ComponentModel.DataAnnotations;
using EmployeesAPI.Domain.Configuration;

namespace EmployeesAPI.Domain.Models
{
    public class Employee
    {
        public Employee()
        {
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public String FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public String LastName { get; set; }

        [Required]
        
        public DateTime BirthDate { get; set; }

        [Required]
        public DateTime EmploymentDate { get; set;}

        [Required]
        public String HomeAddress { get; set; }

        [Required]
        public int Salary { get; set; }

        [Required]
        public RoleTypes Role { get; set; }

        public Employee Boss { get; set; }

        public Guid BossId { get; set; }
    }
}
