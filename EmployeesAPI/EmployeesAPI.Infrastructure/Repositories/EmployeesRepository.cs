using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EmployeesAPI.Domain.Common;
using EmployeesAPI.Domain.Configuration;
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

        public async Task<Response<IEnumerable<Employee>>> GetByParameterAsync(Expression<Func<Employee, bool>> searchCriteria)
        {
            return new Response<IEnumerable<Employee>>(await _context.Employees.Where(searchCriteria).ToListAsync());

        }

        // get by role, average salary, employee count

        public async Task<Response<Employee>> AddAsync(Employee employee)
        {
            var entity = await _context.Employees.AddAsync(employee);

            if (entity.Entity == null)
                return new Response<Employee>("New employee was not added.");
           
            if (await _context.SaveChangesAsync() > 0)
                return new Response<Employee>(entity.Entity);

            return new Response<Employee>("New employee was not added.");
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
            var entity = await _context.Employees.FindAsync(id);

            if (entity == null)
                return new Response<Employee>("This employee was not found!", ErrorCodeTypes.NotFound);

            var deleted = _context.Employees.Remove(entity);

            if (deleted.Entity == null)
                return new Response<Employee>("This employee was not found!", ErrorCodeTypes.NotFound);

            if (await _context.SaveChangesAsync() > 0)
                return new Response<Employee>(deleted.Entity);

            return new Response<Employee>("Failed to save deletion of employee " + id, ErrorCodeTypes.Exception);
        }
    }
}
