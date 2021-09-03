using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeesAPI.Domain.Common;
using EmployeesAPI.Domain.Interfaces;
using EmployeesAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.Infrastructure.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly EmployeesDbContext _context;

        public EmployeesRepository(EmployeesDbContext context)
        {
            _context = context;
        }

        public async Task<Response<Employee>> GetAsync(Guid id)
        {
            return new Response<Employee>(await _context.Employees.FirstOrDefaultAsync(e => e.Id.Equals(id)));
        }

        public async Task<Response<IEnumerable<Employee>>> GetAllAsync()
        {
            return new Response<IEnumerable<Employee>>(await _context.Employees.ToListAsync());

        }

        public async Task<Response<IEnumerable<Employee>>> GetByParameterAsync()
        {
            return new Response<IEnumerable<Employee>>(await _context.Employees.ToListAsync());

        }

        // get by role, average salary, employee count
        public async Task<Response<Employee>> AddAsync(Employee employee)
        {
            return new Response<Employee>(await _context.Employees.FindAsync());

        }

        public async Task<Response<Employee>> UpdateAsync(Employee employee)
        {
            return new Response<Employee>(await _context.Employees.FindAsync());

        }

        public async Task<Response<Employee>> UpdateSalaryAsync(Employee employee)
        {
            return new Response<Employee>(await _context.Employees.FindAsync());

        }

        public async Task<Response<Employee>> DeleteAsync(Guid id)
        {
            return new Response<Employee>(await _context.Employees.FindAsync());

        }
    }
}
