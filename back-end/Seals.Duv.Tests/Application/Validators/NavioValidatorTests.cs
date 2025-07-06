using Seals.Duv.Application.Constants;
using Seals.Duv.Application.Exceptions;
using Seals.Duv.Application.Validators;
using Seals.Duv.Domain.Entities;

namespace Seals.Duv.Tests.Application.Validators;

public class NavioValidatorTests
{
    private readonly NavioValidator _validator = new();

    [Fact(DisplayName = "Should not throw when ship is valid")]
    public void Validate_ShouldNotThrow_WhenValid()
    {
        // Arrange
        var navio = CreateValidNavio();

        // Act & Assert
        var ex = Record.Exception(() => _validator.Validate(navio));
        Assert.Null(ex);
    }

    [Fact(DisplayName = "Should throw when Nome is empty")]
    public void Validate_ShouldThrow_WhenNomeIsEmpty()
    {
        // Arrange
        var navio = CreateValidNavio();
        navio.Nome = "";

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => _validator.Validate(navio));
        Assert.Single(ex.Errors);
        Assert.Contains(ValidationMessages.NavioNomeObrigatorio, ex.Errors);
    }

    [Fact(DisplayName = "Should throw when Bandeira is empty")]
    public void Validate_ShouldThrow_WhenBandeiraIsEmpty()
    {
        // Arrange
        var navio = CreateValidNavio();
        navio.Bandeira = null!;

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => _validator.Validate(navio));
        Assert.Single(ex.Errors);
        Assert.Contains(ValidationMessages.NavioBandeiraObrigatoria, ex.Errors);
    }

    [Fact(DisplayName = "Should throw when ImagemUrl is empty")]
    public void Validate_ShouldThrow_WhenImagemUrlIsEmpty()
    {
        // Arrange
        var navio = CreateValidNavio();
        navio.ImagemUrl = "   ";

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => _validator.Validate(navio));
        Assert.Single(ex.Errors);
        Assert.Contains(ValidationMessages.NavioImagemObrigatoria, ex.Errors);
    }

    [Fact(DisplayName = "Should throw all validation errors when all fields are invalid")]
    public void Validate_ShouldThrowAll_WhenAllFieldsInvalid()
    {
        // Arrange
        var navio = new Navio
        {
            Nome = "",
            Bandeira = "",
            ImagemUrl = ""
        };

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => _validator.Validate(navio));
        Assert.Equal(3, ex.Errors.Count());
        Assert.Contains(ValidationMessages.NavioNomeObrigatorio, ex.Errors);
        Assert.Contains(ValidationMessages.NavioBandeiraObrigatoria, ex.Errors);
        Assert.Contains(ValidationMessages.NavioImagemObrigatoria, ex.Errors);
    }

    private static Navio CreateValidNavio() => new()
    {
        Id = 1,
        Nome = "Navio Azul",
        Bandeira = "Brasil",
        ImagemUrl = "http://imagem.com/navio.jpg"
    };
}
