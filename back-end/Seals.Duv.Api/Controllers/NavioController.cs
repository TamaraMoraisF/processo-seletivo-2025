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

        [HttpGet("{guid}")]
        public async Task<ActionResult<NavioDto>> GetByGuid(Guid guid)
        {
            var item = await _application.GetByGuidAsync(guid);
            return item is not null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<NavioDto>> Create(CreateNavioDto dto)
        {
            var created = await _application.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByGuid), new { guid = created.NavioGuid }, created);
        }

        [HttpPut("{guid}")]
        public async Task<IActionResult> Update(Guid guid, UpdateNavioDto dto)
        {
            await _application.UpdateByGuidAsync(guid, dto);
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
