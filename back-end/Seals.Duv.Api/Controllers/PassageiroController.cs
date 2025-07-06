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

        [HttpGet("{guid}")]
        public async Task<ActionResult<PassageiroDto>> GetByGuid(Guid guid)
        {
            var result = await _application.GetByGuidAsync(guid);
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PassageiroDto>> Create(CreatePassageiroDto dto)
        {
            var created = await _application.CreateAsync(dto);
            if (created is null)
                return NotFound("DUV not found for the provided DuvGuid");

            return CreatedAtAction(nameof(GetByGuid), new { guid = created.PassageiroGuid }, created);
        }

        [HttpPut("{guid}")]
        public async Task<IActionResult> Update(Guid guid, UpdatePassageiroDto dto)
        {
            var success = await _application.UpdateByGuidAsync(guid, dto);
            if (!success)
                return NotFound("DUV not found for the provided DuvGuid");

            return NoContent();
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            await _application.DeleteByGuidAsync(guid);
            return NoContent();
        }
    }
}
