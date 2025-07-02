namespace Seals.Duv.Domain.Entities

{
    public class Duv
    {
        public int Id { get; set; }
        public required string Numero { get; set; }
        public DateTime DataViagem { get; set; }

        public int NavioId { get; set; }
        public Navio? Navio { get; set; }

        public ICollection<Passageiro> Passageiros { get; set; } = new List<Passageiro>();
    }
}