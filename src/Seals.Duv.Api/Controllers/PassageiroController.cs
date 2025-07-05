using Microsoft.AspNetCore.Mvc;
using Seals.Duv.Application.DTOs;
using Seals.Duv.Application.Interfaces;

namespace Seals.Duv.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassageiroController(IPassageiroApplication application) : ControllerBase
    {
        private readonly IPassageiroApplication _application = application;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PassageiroDto>>> GetAll()
        {
            var items = await _application.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PassageiroDto>> GetById(int id)
        {
            var result = await _application.GetByIdAsync(id);
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PassageiroDto>> Create(PassageiroDto dto)
        {
            var created = await _application.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PassageiroDto dto)
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
