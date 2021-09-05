using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeesAPI.Domain.Common;
using EmployeesAPI.Domain.Configuration;
using EmployeesAPI.Domain.Interfaces;
using EmployeesAPI.Domain.Models;

namespace EmployeesAPI.Domain.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _repository;

        public EmployeesService(IEmployeesRepository repository)
        {
            _repository = repository;
        }


        public async Task<Response<Employee>> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<Response<IEnumerable<Employee>>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Response<IEnumerable<Employee>>> GetByParameterAsync()
        {
            return await _repository.GetByParameterAsync(e => e.Role == RoleTypes.Employee);
        }


        public async Task<Response<Employee>> AddAsync(Employee employee)
        {
            if (employee.FirstName.Equals(employee.LastName))
                return new Response<Employee>("First name cannot be the same as last name.");

            int employeeAge = employee.GetAge();

            if (employeeAge < 18)
                return new Response<Employee>("Employee must be at least 18 years old.");

            if (employeeAge > 70) 
                return new Response<Employee>("Employee must be not older than 70 years old.");

            if (employee.EmploymentDate.CompareTo(new DateTime(2000, 1, 1, 0, 0, 0)) < 0) 
                return new Response<Employee>("Employment date cannot be earlier than 2000-01-01.");

            if (employee.EmploymentDate > DateTime.Now)
                return new Response<Employee>("Employment date cannot be in the future.");

            if(employee.Salary < 0)
                return new Response<Employee>("Current salary must be bigger than 0.");

            if (employee.Role != RoleTypes.CEO && employee.BossId == Guid.Empty)
                return new Response<Employee>("Regular employee must have boss. Please fill it.");

            if (employee.Role == RoleTypes.CEO)
            {
                var ceoEmployee = await _repository.GetByParameterAsync(e => e.Role == RoleTypes.CEO);

                if (!ceoEmployee.IsSuccess)
                    return new Response<Employee>(ceoEmployee.InnerException, ceoEmployee.ErrorMessages, ceoEmployee.ErrorCodes);

                if (ceoEmployee.Content.Count() > 0)
                    return new Response<Employee>("Only one employee can have CEO role.");
            }

            return await _repository.AddAsync(employee);
        }

        public async Task<Response<Employee>> UpdateAsync(Employee employee)
        {
            return await _repository.UpdateAsync(employee);
        }

        public async Task<Response<Employee>> UpdateSalaryAsync(Employee employee)
        {
            return await _repository.UpdateSalaryAsync(employee);
        }

        public async Task<Response<Employee>> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

    }
}
