using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public EmployeesController(IEmployeesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAllAsync();

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _service.GetAsync(id);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("search/")]
        public async Task<IActionResult> Get(string name, DateTime? birthDateFrom, DateTime? birthDateTo)
        {
            var result = await _service.SearchAsync(name, birthDateFrom, birthDateTo);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("boss/{bossId}")]
        public async Task<IActionResult> GetByBoss(Guid bossId)
        {
            var result = await _service.GetByBossIdAsync(bossId);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("role/{role}/Statistics")]
        public async Task<IActionResult> GetStatistics(RoleTypes role)
        {
            var result = await _service.GetStatisticsAsync(role);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult>  Post([FromBody]Employee employee)
        {
            var result = await _service.AddAsync(employee);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Employee employee)
        {
            var result = await _service.UpdateAsync(employee);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPut("{id}/Salary/{newSalary}")]
        public async Task<IActionResult> PutSalary(Guid id, int newSalary)
        {
            var result = await _service.UpdateSalaryAsync(id, newSalary);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]Guid id)
        {
            var result = await _service.DeleteAsync(id);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
