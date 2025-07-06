namespace Seals.Duv.Domain.Entities
{
    public class Navio
    {
        public int Id { get; set; }
        public Guid NavioGuid { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string Bandeira { get; set; } = string.Empty;
        public string ImagemUrl { get; set; } = string.Empty;
        public ICollection<Duv> Duvs { get; set; } = [];
    }
}
