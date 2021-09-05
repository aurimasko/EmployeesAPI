﻿using System;
using System.Threading.Tasks;
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
            var result = await _service.GetByParameterAsync(e => e.Id.Equals(id));

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("Search/{name}/{birthDateFrom}/{birthDateTo}")]
        public async Task<IActionResult> Get(String Name, DateTime birthDateFrom, DateTime birthDateTo)
        { 
            var result = await _service.GetByParameterAsync(e => e.FirstName.Equals(Name) && e.BirthDate >= birthDateFrom.Date && e.BirthDate <= birthDateTo.Date);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("Boss/{bossId}")]
        public async Task<IActionResult> GetByBoss(Guid bossId)
        {
            var result = await _service.GetByParameterAsync(e => e.BossId == bossId);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        // not working yet
        [HttpGet("{role}")]
        public async Task<IActionResult> GetCount(RoleTypes role)
        {
            var result = await _service.GetByParameterAsync(e => e.Role == role);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Employee employee)
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

        // change employee to Id, Salary.
        [HttpPut("test/")]
        public async Task<IActionResult> PutSalary([FromBody]Employee employee)
        {
            var result = await _service.UpdateSalaryAsync(employee);

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
