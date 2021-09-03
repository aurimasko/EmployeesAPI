using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeesAPI.Domain.Common;
using EmployeesAPI.Domain.Models;

namespace EmployeesAPI.Domain.Interfaces
{
    public interface IEmployeesService
    {
        Task<Response<Employee>> GetAsync(Guid id);
        Task<Response<IEnumerable<Employee>>> GetAllAsync();
        Task<Response<IEnumerable<Employee>>> GetByParameterAsync();
        // get by role, average salary, employee count
        Task<Response<Employee>> AddAsync(Employee employee);
        Task<Response<Employee>> UpdateAsync(Employee employee);
        Task<Response<Employee>> UpdateSalaryAsync(Employee employee);
        Task<Response<Employee>> DeleteAsync(Guid id);
    }
}
