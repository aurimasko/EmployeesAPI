using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeesAPI.API.DTO;
using EmployeesAPI.API.Extensions;
using EmployeesAPI.Domain.Common;
using EmployeesAPI.Domain.Configuration;
using EmployeesAPI.Domain.Interfaces;
using EmployeesAPI.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesAPI.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _service;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeesService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAllAsync();
            var mappedResult = _mapper.ToDTO<EmployeeDTO, Employee>(result);

            if (mappedResult.IsSuccess)
                return Ok(mappedResult);
            else
                return BadRequest(mappedResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _service.GetAsync(id);
            var mappedResult = _mapper.ToDTO<EmployeeDTO, Employee>(result);

            if (mappedResult.IsSuccess)
                return Ok(mappedResult);
            else
                return BadRequest(mappedResult);
        }

        [HttpGet("Search/")]
        public async Task<IActionResult> Get(string name, DateTime? birthDateFrom, DateTime? birthDateTo)
        {
            DateTime bdFrom = birthDateFrom ?? DateTime.MinValue;
            DateTime bdTo = birthDateTo ?? DateTime.MaxValue;

            var result = await _service.GetByParameterAsync(e => (e.FirstName.Equals(name) || name == String.Empty) && e.BirthDate >= bdFrom.Date && e.BirthDate <= bdTo.Date);
            var mappedResult = _mapper.ToDTO<EmployeeDTO, Employee>(result);

            if (mappedResult.IsSuccess)
                return Ok(mappedResult);
            else
                return BadRequest(mappedResult);
        }

        [HttpGet("Boss/{bossId}")]
        public async Task<IActionResult> GetByBoss(Guid bossId)
        {
            var result = await _service.GetByParameterAsync(e => e.BossId == bossId);
            var mappedResult = _mapper.ToDTO<EmployeeDTO, Employee>(result);

            if (mappedResult.IsSuccess)
                return Ok(mappedResult);
            else
                return BadRequest(mappedResult);
        }

        [HttpGet("Role/{role}/Statistics")]
        public async Task<IActionResult> GetStatistics(RoleTypes role)
        {
            var result = await _service.GetStatisticsAsync(role);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]EmployeeDTO employee)
        {
            var mappedEmployee = _mapper.Map<Employee>(employee);
            var result = await _service.AddAsync(mappedEmployee);
            var mappedResult = _mapper.ToDTO<EmployeeDTO, Employee>(result);

            if (mappedResult.IsSuccess)
                return Ok(mappedResult);
            else
                return BadRequest(mappedResult);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EmployeeDTO employee)
        {
            var mappedEmployee = _mapper.Map<Employee>(employee);
            var result = await _service.UpdateAsync(mappedEmployee);
            var mappedResult = _mapper.ToDTO<EmployeeDTO, Employee>(result);

            if (mappedResult.IsSuccess)
                return Ok(mappedResult);
            else
                return BadRequest(mappedResult);
        }

        [HttpPut("{id}/Salary/{newSalary}")]
        public async Task<IActionResult> PutSalary(Guid id, int newSalary)
        {
            var result = await _service.UpdateSalaryAsync(id, newSalary);
            var mappedResult = _mapper.ToDTO<EmployeeDTO, Employee>(result);

            if (mappedResult.IsSuccess)
                return Ok(mappedResult);
            else
                return BadRequest(mappedResult);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]Guid id)
        {
            var result = await _service.DeleteAsync(id);
            var mappedResult = _mapper.ToDTO<EmployeeDTO, Employee>(result);

            if (mappedResult.IsSuccess)
                return Ok(mappedResult);
            else
                return BadRequest(mappedResult);
        }
    }
}
