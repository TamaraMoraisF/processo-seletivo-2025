﻿namespace Seals.Duv.Application.DTOs
{
    public class DuvDto
    {
        public Guid DuvGuid { get; set; }
        public string Numero { get; set; } = string.Empty;
        public DateTime DataViagem { get; set; }
        public Guid NavioGuid { get; set; }
        public NavioDto? Navio { get; set; }
        public IEnumerable<PassageiroDto> Passageiros { get; set; } = [];
    }
}
