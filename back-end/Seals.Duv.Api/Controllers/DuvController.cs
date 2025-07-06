using Microsoft.AspNetCore.Mvc;
using Seals.Duv.Application.DTOs;
using Seals.Duv.Application.Interfaces;

namespace Seals.Duv.Api.Controllers;

/// <summary>
/// Gerencia operações relacionadas a DUVs.
/// </summary>
[ApiController]
[Route("api/duvs")]
[Produces("application/json")]
public class DuvController(IDuvApplication application) : ControllerBase
{
    private readonly IDuvApplication _application = application;

    /// <summary>
    /// Lista todas as DUVs cadastradas.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DuvDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DuvDto>>> GetAll()
    {
        var items = await _application.GetAllAsync();
        return Ok(items);
    }

    /// <summary>
    /// Retorna uma DUV pelo GUID.
    /// </summary>
    [HttpGet("{guid}")]
    [ProducesResponseType(typeof(DuvDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DuvDto>> GetByGuid(Guid guid)
    {
        var item = await _application.GetByGuidAsync(guid);
        return item is not null ? Ok(item) : NotFound();
    }

    /// <summary>
    /// Cria uma nova DUV.
    /// </summary>
    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(DuvDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DuvDto>> Create(CreateDuvDto dto)
    {
        var created = await _application.CreateAsync(dto);

        if (created is null)
            return NotFound("Navio not found for the provided NavioGuid");

        return CreatedAtAction(nameof(GetByGuid), new { guid = created.DuvGuid }, created);
    }

    /// <summary>
    /// Atualiza uma DUV existente.
    /// </summary>
    [HttpPut("{guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid guid, UpdateDuvDto dto)
    {
        var updated = await _application.UpdateByGuidAsync(guid, dto);

        if (!updated)
            return NotFound("Navio not found for the provided NavioGuid");

        return NoContent();
    }

    /// <summary>
    /// Remove uma DUV pelo GUID.
    /// </summary>
    [HttpDelete("{guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid guid)
    {
        await _application.DeleteByGuidAsync(guid);
        return NoContent();
    }
}
