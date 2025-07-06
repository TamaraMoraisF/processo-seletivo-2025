namespace Seals.Duv.Application.Constants
{
    public static class ValidationMessages
    {
        // Navio
        public const string NavioNomeObrigatorio = "Nome do navio é obrigatório.";
        public const string NavioBandeiraObrigatoria = "Bandeira do navio é obrigatória.";
        public const string NavioImagemObrigatoria = "Imagem do navio é obrigatória.";

        // DUV
        public const string DuvNumeroObrigatorio = "Número da DUV é obrigatório.";
        public const string DuvDataObrigatoria = "Data da viagem é obrigatória.";
        public const string DuvNavioIdInvalido = "NavioId inválido.";

        // Passageiro
        public const string PassageiroNomeObrigatorio = "Nome do passageiro é obrigatório.";
        public const string PassageiroNacionalidadeObrigatoria = "Nacionalidade é obrigatória.";
        public const string PassageiroFotoObrigatoria = "Foto do passageiro é obrigatória.";
        public const string PassageiroSidObrigatorioParaTripulante = "Tripulantes devem ter um documento SID.";
        public const string PassageiroDuvIdInvalido = "DuvId inválido.";
    }
}
