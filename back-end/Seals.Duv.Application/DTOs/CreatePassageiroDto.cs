using Seals.Duv.Domain.Enum;

namespace Seals.Duv.Application.DTOs
{
    public class CreatePassageiroDto
    {
        public string Nome { get; set; } = string.Empty;
        public TipoPassageiro Tipo { get; set; }
        public string Nacionalidade { get; set; } = string.Empty;
        public string FotoUrl { get; set; } = string.Empty;
        public string? Sid { get; set; }
        public Guid DuvGuid { get; set; }
    }
}
