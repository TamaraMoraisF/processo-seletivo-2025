using Seals.Duv.Application.Constants;
using Seals.Duv.Application.Exceptions;
using Seals.Duv.Application.Validators;
using Seals.Duv.Domain.Entities;
using Seals.Duv.Domain.Enum;

namespace Seals.Duv.Tests.Application.Validators;

public class PassageiroValidatorTests
{
    private readonly PassageiroValidator _validator = new();

    [Fact(DisplayName = "Should not throw when passenger is valid")]
    public void Validate_ShouldNotThrow_WhenValid()
    {
        // Arrange
        var passageiro = CreateValidPassageiro();

        // Act & Assert
        var ex = Record.Exception(() => _validator.Validate(passageiro));
        Assert.Null(ex);
    }

    [Fact(DisplayName = "Should throw if name is empty")]
    public void Validate_ShouldThrow_WhenNomeIsEmpty()
    {
        // Arrange
        var passageiro = CreateValidPassageiro();
        passageiro.Nome = "";

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => _validator.Validate(passageiro));
        Assert.Single(ex.Errors);
        Assert.Contains(ValidationMessages.PassageiroNomeObrigatorio, ex.Errors);
    }

    [Fact(DisplayName = "Should throw if nationality is empty")]
    public void Validate_ShouldThrow_WhenNacionalidadeIsEmpty()
    {
        // Arrange
        var passageiro = CreateValidPassageiro();
        passageiro.Nacionalidade = " ";

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => _validator.Validate(passageiro));
        Assert.Single(ex.Errors);
        Assert.Contains(ValidationMessages.PassageiroNacionalidadeObrigatoria, ex.Errors);
    }

    [Fact(DisplayName = "Should throw if photo URL is empty")]
    public void Validate_ShouldThrow_WhenFotoUrlIsEmpty()
    {
        // Arrange
        var passageiro = CreateValidPassageiro();
        passageiro.FotoUrl = null!;

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => _validator.Validate(passageiro));
        Assert.Single(ex.Errors);
        Assert.Contains(ValidationMessages.PassageiroFotoObrigatoria, ex.Errors);
    }

    [Fact(DisplayName = "Should throw if SID is missing for crew member")]
    public void Validate_ShouldThrow_WhenTipoIsTripulanteAndSidIsEmpty()
    {
        // Arrange
        var passageiro = CreateValidPassageiro();
        passageiro.Tipo = TipoPassageiro.Tripulante;
        passageiro.Sid = "";

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => _validator.Validate(passageiro));
        Assert.Single(ex.Errors);
        Assert.Contains(ValidationMessages.PassageiroSidObrigatorioParaTripulante, ex.Errors);
    }

    [Fact(DisplayName = "Should throw if DuvId is invalid")]
    public void Validate_ShouldThrow_WhenDuvIdIsInvalid()
    {
        // Arrange
        var passageiro = CreateValidPassageiro();
        passageiro.DuvId = 0;

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => _validator.Validate(passageiro));
        Assert.Single(ex.Errors);
        Assert.Contains(ValidationMessages.PassageiroDuvIdInvalido, ex.Errors);
    }

    [Fact(DisplayName = "Should throw all validation errors when all invalid")]
    public void Validate_ShouldThrowAll_WhenAllFieldsAreInvalid()
    {
        // Arrange
        var passageiro = new Passageiro
        {
            Nome = "",
            Nacionalidade = "",
            FotoUrl = "",
            Tipo = TipoPassageiro.Tripulante,
            Sid = "",
            DuvId = 0
        };

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => _validator.Validate(passageiro));
        Assert.Equal(5, ex.Errors.Count());
        Assert.Contains(ValidationMessages.PassageiroNomeObrigatorio, ex.Errors);
        Assert.Contains(ValidationMessages.PassageiroNacionalidadeObrigatoria, ex.Errors);
        Assert.Contains(ValidationMessages.PassageiroFotoObrigatoria, ex.Errors);
        Assert.Contains(ValidationMessages.PassageiroSidObrigatorioParaTripulante, ex.Errors);
        Assert.Contains(ValidationMessages.PassageiroDuvIdInvalido, ex.Errors);
    }

    private static Passageiro CreateValidPassageiro() => new()
    {
        Id = 1,
        Nome = "João",
        Nacionalidade = "Brasileira",
        FotoUrl = "http://imagem.com/foto.jpg",
        Tipo = TipoPassageiro.Passageiro,
        DuvId = 10,
        Sid = null
    };
}
