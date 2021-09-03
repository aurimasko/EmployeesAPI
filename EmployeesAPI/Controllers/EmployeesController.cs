using System;
using System.Threading.Tasks;
using EmployeesAPI.Domain.Interfaces;
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

        [HttpGet("id")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _service.GetAsync(id);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

    }
}
