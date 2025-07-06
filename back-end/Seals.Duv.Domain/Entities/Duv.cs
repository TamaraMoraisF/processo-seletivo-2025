namespace Seals.Duv.Domain.Entities
{
    public class Duv
    {
        public int Id { get; set; }
        public Guid DuvGuid { get; set; } = Guid.NewGuid();
        public string Numero { get; set; } = string.Empty;
        public DateTime DataViagem { get; set; }
        public int NavioId { get; set; }
        public Navio Navio { get; set; } = null!;
        public ICollection<Passageiro> Passageiros { get; set; } = [];
    }
}
