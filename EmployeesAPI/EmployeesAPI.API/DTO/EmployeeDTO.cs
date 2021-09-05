using System;
using System.ComponentModel.DataAnnotations;
using EmployeesAPI.Domain.Configuration;

namespace EmployeesAPI.API.DTO
{
    public class EmployeeDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EmploymentDate { get; set; }

        [Required]
        public string HomeAddress { get; set; }

        [Required]
        public int Salary { get; set; }

        [Required]
        public RoleTypes Role { get; set; }

        public Guid? BossId { get; set; }

        //For tracking concurrency changes
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}

