using Microsoft.AspNetCore.Mvc;
using Seals.Duv.Application.DTOs;
using Seals.Duv.Application.Interfaces;

namespace Seals.Duv.Api.Controllers;

/// <summary>
/// Gerencia operações relacionadas a navios.
/// </summary>
[ApiController]
[Route("api/navios")]
[Produces("application/json")]
public class NavioController(INavioApplication application) : ControllerBase
{
    private readonly INavioApplication _application = application;

    /// <summary>
    /// Lista todos os navios cadastrados.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<NavioDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<NavioDto>>> GetAll()
    {
        var items = await _application.GetAllAsync();
        return Ok(items);
    }

    /// <summary>
    /// Retorna um navio pelo GUID.
    /// </summary>
    [HttpGet("{guid}")]
    [ProducesResponseType(typeof(NavioDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<NavioDto>> GetByGuid(Guid guid)
    {
        var item = await _application.GetByGuidAsync(guid);
        return item is not null ? Ok(item) : NotFound();
    }

    /// <summary>
    /// Cria um novo navio.
    /// </summary>
    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(NavioDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<NavioDto>> Create(CreateNavioDto dto)
    {
        var created = await _application.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByGuid), new { guid = created.NavioGuid }, created);
    }

    /// <summary>
    /// Atualiza um navio existente.
    /// </summary>
    [HttpPut("{guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update(Guid guid, UpdateNavioDto dto)
    {
        await _application.UpdateByGuidAsync(guid, dto);
        return NoContent();
    }

    /// <summary>
    /// Remove um navio pelo GUID.
    /// </summary>
    [HttpDelete("{guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid guid)
    {
        await _application.DeleteByGuidAsync(guid);
        return NoContent();
    }
}
