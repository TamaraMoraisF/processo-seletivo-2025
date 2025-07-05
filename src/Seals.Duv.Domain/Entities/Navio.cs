using System.Collections.Generic;

namespace Seals.Duv.Domain.Entities
{
    public class Navio
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;
        public string Bandeira { get; set; } = string.Empty;
        public string ImagemUrl { get; set; } = string.Empty;

        public List<Duv> Duvs { get; set; } = new();
    }
}