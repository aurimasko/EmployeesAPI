using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EmployeesAPI.Domain.Common;
using EmployeesAPI.Domain.Models;

namespace EmployeesAPI.Domain.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<Response<Employee>> GetAsync(Guid id);
        Task<Response<IEnumerable<Employee>>> GetAllAsync();
        Task<Response<IEnumerable<Employee>>> GetByParameterAsync(Expression<Func<Employee, bool>> searchCriteria);
        // get by role, average salary, employee count
        Task<Response<Employee>> AddAsync(Employee employee);
        Task<Response<Employee>> UpdateAsync(Employee employee);
        Task<Response<Employee>> UpdateSalaryAsync(Employee employee);
        Task<Response<Employee>> DeleteAsync(Guid id);
    }
}
