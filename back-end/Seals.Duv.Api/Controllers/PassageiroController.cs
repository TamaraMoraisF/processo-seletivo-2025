using Microsoft.AspNetCore.Mvc;
using Seals.Duv.Application.DTOs;
using Seals.Duv.Application.Interfaces;

namespace Seals.Duv.Api.Controllers;

/// <summary>
/// Gerencia operações relacionadas a passageiros.
/// </summary>
[ApiController]
[Route("api/passageiros")]
[Produces("application/json")]
public class PassageiroController(IPassageiroApplication application) : ControllerBase
{
    private readonly IPassageiroApplication _application = application;

    /// <summary>
    /// Lista todos os passageiros cadastrados.
    /// </summary>
    /// <returns>Lista de passageiros.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PassageiroDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PassageiroDto>>> GetAllAsync()
    {
        var items = await _application.GetAllAsync();
        return Ok(items);
    }

    /// <summary>
    /// Retorna um passageiro pelo Guid.
    /// </summary>
    /// <param name="guid">GUID do passageiro.</param>
    /// <returns>Dados do passageiro.</returns>
    [HttpGet("{guid}")]
    [ProducesResponseType(typeof(PassageiroDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PassageiroDto>> GetByGuidAsync(Guid guid)
    {
        var result = await _application.GetByGuidAsync(guid);
        return result is not null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Cria um novo passageiro.
    /// </summary>
    /// <param name="dto">Dados do passageiro a ser criado.</param>
    /// <returns>Passageiro criado.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(PassageiroDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PassageiroDto>> CreateAsync([FromBody] CreatePassageiroDto dto)
    {
        var created = await _application.CreateAsync(dto);
        return created is null
            ? NotFound("DUV not found for the provided DuvGuid")
            : CreatedAtAction(nameof(GetByGuidAsync), new { guid = created.PassageiroGuid }, created);
    }

    /// <summary>
    /// Atualiza os dados de um passageiro existente.
    /// </summary>
    /// <param name="guid">GUID do passageiro.</param>
    /// <param name="dto">Dados atualizados.</param>
    [HttpPut("{guid}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(Guid guid, [FromBody] UpdatePassageiroDto dto)
    {
        var success = await _application.UpdateByGuidAsync(guid, dto);
        return !success
            ? NotFound("DUV not found for the provided DuvGuid")
            : NoContent();
    }

    /// <summary>
    /// Remove um passageiro pelo Guid.
    /// </summary>
    /// <param name="guid">GUID do passageiro.</param>
    [HttpDelete("{guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync(Guid guid)
    {
        await _application.DeleteByGuidAsync(guid);
        return NoContent();
    }
}
