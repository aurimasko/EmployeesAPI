using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeesAPI.Domain.Common;
using EmployeesAPI.Domain.Configuration;

namespace EmployeesAPI.Domain.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public String FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public String LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EmploymentDate { get; set;}

        [Required]
        public String HomeAddress { get; set; }

        [Required]
        public int Salary { get; set; }

        [Required]
        public RoleTypes Role { get; set; }

        [ForeignKey("BossId")]
        public Employee Boss { get; set; }

        public Guid? BossId { get; set; }

        //For tracking concurrency changes
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int GetAge()
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;

            // Go back to the year in which the person was born in case of a leap year
            if (BirthDate.Date > today.AddYears(-age))
                age--;

            return age; 
        }
    }
}
