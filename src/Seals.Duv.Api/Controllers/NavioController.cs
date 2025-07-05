using Microsoft.AspNetCore.Mvc;
using Seals.Duv.Application.DTOs;
using Seals.Duv.Application.Interfaces;

namespace Seals.Duv.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NavioController(INavioApplication application) : ControllerBase
    {
        private readonly INavioApplication _application = application;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NavioDto>>> GetAll()
        {
            var items = await _application.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NavioDto>> GetById(int id)
        {
            var item = await _application.GetByIdAsync(id);
            return item is not null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<NavioDto>> Create(NavioDto dto)
        {
            var created = await _application.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NavioDto dto)
        {
            await _application.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _application.DeleteAsync(id);
            return NoContent();
        }
    }
}
