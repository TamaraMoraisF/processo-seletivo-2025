using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seals.Duv.Domain.Entities;
using Seals.Duv.Infrastructure.Persistence;

namespace Seals.Duv.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NavioController(DuvDbContext context) : ControllerBase
    {
        private readonly DuvDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Navio>>> GetAll()
        {
            return await _context.Navios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Navio>> GetById(int id)
        {
            var navio = await _context.Navios.FindAsync(id);
            if (navio == null) return NotFound();
            return navio;
        }

        [HttpPost]
        public async Task<ActionResult<Navio>> Create(Navio navio)
        {
            _context.Navios.Add(navio);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = navio.Id }, navio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Navio navio)
        {
            var navioExistente = await _context.Navios.FindAsync(id);
            if (navioExistente == null) return NotFound();

            navioExistente.Nome = navio.Nome;
            navioExistente.Bandeira = navio.Bandeira;
            navioExistente.ImagemUrl = navio.ImagemUrl;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var navio = await _context.Navios.FindAsync(id);
            if (navio == null) return NotFound();

            _context.Navios.Remove(navio);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
