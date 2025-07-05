namespace Seals.Duv.Domain.Entities
{
    public class Duv
    {
        public int Id { get; set; }

        public string Numero { get; set; } = string.Empty;
        public DateTime DataViagem { get; set; }

        public int NavioId { get; set; }
        public Navio Navio { get; set; } = null!;

        public List<Passageiro> Passageiros { get; set; } = new();
    }
}