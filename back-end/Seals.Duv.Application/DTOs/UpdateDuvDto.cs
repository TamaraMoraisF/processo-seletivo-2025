namespace Seals.Duv.Application.DTOs
{
    public class UpdateDuvDto
    {
        public string Numero { get; set; } = string.Empty;
        public DateTime DataViagem { get; set; }
        public Guid NavioGuid { get; set; }
        public ICollection<PassageiroDuvDto> Passageiros { get; set; } = [];
    }
}
