using System.Collections.Generic;

namespace Seals.Duv.Domain.Entities
{
    public class Navio
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Bandeira { get; set; }
        public required string ImagemUrl { get; set; }

        public ICollection<Duv> Duvs { get; set; } = new List<Duv>();
    }
}