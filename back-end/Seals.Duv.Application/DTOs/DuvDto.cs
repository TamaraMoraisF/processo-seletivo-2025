namespace Seals.Duv.Application.DTOs
{
    public class DuvDto
    {
        public int Id { get; set; }
        public string Numero { get; set; } = string.Empty;
        public DateTime DataViagem { get; set; }
        public int NavioId { get; set; }
        public NavioDto? Navio { get; set; }
        public IEnumerable<PassageiroDto> Passageiros { get; set; } = [];
    }
}
