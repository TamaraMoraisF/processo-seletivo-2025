using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seals.Duv.Infrastructure.Persistence;

namespace Seals.Duv.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DuvController : ControllerBase
    {
        private readonly DuvDbContext _context;

        public DuvController(DuvDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seals.Duv.Domain.Entities.Duv>>> GetAll()
        {
            return await _context.Duvs
                .Include(d => d.Navio)
                .Include(d => d.Passageiros)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetById(int id)
        {
            var duv = await _context.Duvs
                .Include(d => d.Navio)
                .Include(d => d.Passageiros)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (duv == null) return NotFound();

            return new
            {
                duv.Numero,
                duv.DataViagem,
                Navio = duv.Navio,
                Passageiros = duv.Passageiros
                    .GroupBy(p => p.Tipo)
                    .ToDictionary(g => g.Key.ToString(), g => g.ToList())
            };
        }

        [HttpGet("{id}/completo")]
        public async Task<ActionResult<Seals.Duv.Domain.Entities.Duv>> GetDuvCompleta(int id)
        {
            var duv = await _context.Duvs
                .Include(d => d.Navio)
                .Include(d => d.Passageiros)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (duv == null) return NotFound();

            return duv;
        }

        [HttpPost]
        public async Task<ActionResult<Seals.Duv.Domain.Entities.Duv>> Create(Seals.Duv.Domain.Entities.Duv duv)
        {
            _context.Duvs.Add(duv);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = duv.Id }, duv);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Seals.Duv.Domain.Entities.Duv duv)
        {
            if (id != duv.Id) return BadRequest();

            _context.Entry(duv).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
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