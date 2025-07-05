using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seals.Duv.Infrastructure.Persistence;

namespace Seals.Duv.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DuvController(DuvDbContext context) : ControllerBase
    {
        private readonly DuvDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.Entities.Duv>>> GetAll()
        {
            var duvs = await _context.Duvs
                .Include(d => d.Navio)
                .Include(d => d.Passageiros)
                .ToListAsync();

            return Ok(duvs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetById(int id)
        {
            var duv = await _context.Duvs
                .Include(d => d.Navio)
                .Include(d => d.Passageiros)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (duv == null) return NotFound();

            return Ok(new
            {
                duv.Id,
                duv.Numero,
                duv.DataViagem,
                duv.Navio,
                Passageiros = duv.Passageiros
                    .GroupBy(p => p.Tipo)
                    .ToDictionary(g => g.Key.ToString(), g => g.ToList())
            });
        }

        [HttpGet("{id}/completo")]
        public async Task<ActionResult<Domain.Entities.Duv>> GetDuvCompleta(int id)
        {
            var duv = await _context.Duvs
                .Include(d => d.Navio)
                .Include(d => d.Passageiros)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (duv == null) return NotFound();

            return Ok(duv);
        }

        [HttpPost]
        public async Task<ActionResult<Domain.Entities.Duv>> Create(Domain.Entities.Duv duv)
        {
            duv.DataViagem = DateTime.SpecifyKind(duv.DataViagem, DateTimeKind.Utc);

            _context.Duvs.Add(duv);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = duv.Id }, duv);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var duv = await _context.Duvs.FindAsync(id);
            if (duv == null) return NotFound();

            _context.Duvs.Remove(duv);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
