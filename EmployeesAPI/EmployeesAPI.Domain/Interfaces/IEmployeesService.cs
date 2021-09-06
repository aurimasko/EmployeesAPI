using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EmployeesAPI.Domain.Common;
using EmployeesAPI.Domain.Configuration;
using EmployeesAPI.Domain.Models;

namespace EmployeesAPI.Domain.Interfaces
{
    public interface IEmployeesService
    {
        Task<Response<Employee>> GetAsync(Guid id);
        Task<Response<IEnumerable<Employee>>> GetAllAsync();
        Task<Response<IEnumerable<Employee>>> SearchAsync(string name, DateTime? birthdateFrom, DateTime? birthdateTo);
        Task<Response<IEnumerable<Employee>>> GetByBossIdAsync(Guid bossId);
        Task<Response<IEnumerable<Employee>>> GetByParameterAsync(Expression<Func<Employee, bool>> searchCriteria);
        Task<Response<EmployeesAnalysis>> GetStatisticsAsync(RoleTypes role);
        Task<Response<Employee>> AddAsync(Employee employee);
        Task<Response<Employee>> UpdateAsync(Employee employee);
        Task<Response<Employee>> UpdateSalaryAsync(Guid id, int newSalary);
        Task<Response<Employee>> DeleteAsync(Guid id);
    }
}
