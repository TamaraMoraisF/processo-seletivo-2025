using Seals.Duv.Domain.Enum;

namespace Seals.Duv.Domain.Entities
{
    public class Passageiro
    {
        public int Id { get; set; }
        public Guid PassageiroGuid { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public TipoPassageiro Tipo { get; set; }
        public string Nacionalidade { get; set; } = string.Empty;
        public string FotoUrl { get; set; } = string.Empty;
        public string? Sid { get; set; }
        public int DuvId { get; set; }
        public Duv Duv { get; set; } = null!;
    }
}
