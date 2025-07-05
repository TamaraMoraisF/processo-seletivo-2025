using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seals.Duv.Domain.Entities;
using Seals.Duv.Infrastructure.Persistence;

namespace Seals.Duv.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassageiroController(DuvDbContext context) : ControllerBase
    {
        private readonly DuvDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passageiro>>> GetAll()
        {
            return await _context.Passageiros.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Passageiro>> GetById(int id)
        {
            var passageiro = await _context.Passageiros.FindAsync(id);
            if (passageiro == null) return NotFound();
            return passageiro;
        }

        [HttpPost]
        public async Task<ActionResult<Passageiro>> Create(Passageiro passageiro)
        {
            _context.Passageiros.Add(passageiro);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = passageiro.Id }, passageiro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Passageiro passageiro)
        {
            var existente = await _context.Passageiros.FindAsync(id);
            if (existente == null) return NotFound();

            existente.Nome = passageiro.Nome;
            existente.Tipo = passageiro.Tipo;
            existente.Nacionalidade = passageiro.Nacionalidade;
            existente.FotoUrl = passageiro.FotoUrl;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var passageiro = await _context.Passageiros.FindAsync(id);
            if (passageiro == null) return NotFound();

            _context.Passageiros.Remove(passageiro);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
