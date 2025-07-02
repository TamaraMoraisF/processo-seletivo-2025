namespace Seals.Duv.Domain.Entities
{
    public enum TipoPassageiro
    {
        Passageiro = 1,
        Tripulante = 2
    }

    public class Passageiro
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public TipoPassageiro Tipo { get; set; }
        public required string Nacionalidade { get; set; }
        public required string FotoUrl { get; set; }
        public string? Sid { get; set; }

        public int DuvId { get; set; }
        public required Duv Duv { get; set; }
    }
}