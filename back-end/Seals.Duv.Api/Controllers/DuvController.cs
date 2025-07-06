using Microsoft.AspNetCore.Mvc;
using Seals.Duv.Application.DTOs;
using Seals.Duv.Application.Interfaces;

namespace Seals.Duv.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DuvController(IDuvApplication application) : ControllerBase
    {
        private readonly IDuvApplication _application = application;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DuvDto>>> GetAll()
        {
            var items = await _application.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<DuvDto>> GetByGuid(Guid guid)
        {
            var item = await _application.GetByGuidAsync(guid);
            return item is not null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<DuvDto>> Create(CreateDuvDto dto)
        {
            var created = await _application.CreateAsync(dto);

            if (created is null)
                return NotFound("Navio not found for the provided NavioGuid");

            return CreatedAtAction(nameof(GetByGuid), new { guid = created.DuvGuid }, created);
        }

        [HttpPut("{guid}")]
        public async Task<IActionResult> Update(Guid guid, UpdateDuvDto dto)
        {
            var updated = await _application.UpdateByGuidAsync(guid, dto);

            if (!updated)
                return NotFound("Navio not found for the provided NavioGuid");

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
