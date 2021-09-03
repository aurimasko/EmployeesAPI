using System;
using Microsoft.EntityFrameworkCore;
using EmployeesAPI.Domain.Models;

namespace EmployeesAPI.Infrastructure
{
    public class EmployeesDbContext : DbContext
    {
        public EmployeesDbContext(DbContextOptions<EmployeesDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
