using Seals.Duv.Application.Constants;
using Seals.Duv.Application.Exceptions;
using Seals.Duv.Application.Validators;

namespace Seals.Duv.Tests.Application.Validators;

public class DuvValidatorTests
{
    private readonly DuvValidator _validator = new();

    [Fact(DisplayName = "Should not throw when DUV is valid")]
    public void Validate_ShouldNotThrow_WhenValid()
    {
        // Arrange
        var duv = CreateValidDuv();

        // Act & Assert
        var ex = Record.Exception(() => _validator.Validate(duv));
        Assert.Null(ex);
    }

    [Fact(DisplayName = "Should throw if Numero is missing")]
    public void Validate_ShouldThrow_WhenNumeroIsEmpty()
    {
        // Arrange
        var duv = CreateValidDuv();
        duv.Numero = "";

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => _validator.Validate(duv));
        Assert.Single(ex.Errors);
        Assert.Contains(ValidationMessages.DuvNumeroObrigatorio, ex.Errors);
    }

    [Fact(DisplayName = "Should throw if DataViagem is default")]
    public void Validate_ShouldThrow_WhenDataViagemIsDefault()
    {
        // Arrange
        var duv = CreateValidDuv();
        duv.DataViagem = default;

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => _validator.Validate(duv));
        Assert.Single(ex.Errors);
        Assert.Contains(ValidationMessages.DuvDataObrigatoria, ex.Errors);
    }

    [Fact(DisplayName = "Should throw if NavioId is invalid")]
    public void Validate_ShouldThrow_WhenNavioIdIsInvalid()
    {
        // Arrange
        var duv = CreateValidDuv();
        duv.NavioId = 0;

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => _validator.Validate(duv));
        Assert.Single(ex.Errors);
        Assert.Contains(ValidationMessages.DuvNavioIdInvalido, ex.Errors);
    }

    [Fact(DisplayName = "Should throw all validation errors together")]
    public void Validate_ShouldThrowAllErrors_WhenAllInvalid()
    {
        // Arrange
        var duv = new Domain.Entities.Duv
        {
            Numero = "",
            DataViagem = default,
            NavioId = -1
        };

        // Act & Assert
        var ex = Assert.Throws<ValidationException>(() => _validator.Validate(duv));
        Assert.Equal(3, ex.Errors.Count());
        Assert.Contains(ValidationMessages.DuvNumeroObrigatorio, ex.Errors);
        Assert.Contains(ValidationMessages.DuvDataObrigatoria, ex.Errors);
        Assert.Contains(ValidationMessages.DuvNavioIdInvalido, ex.Errors);
    }

    private static Domain.Entities.Duv CreateValidDuv() => new()
    {
        Id = 1,
        Numero = "DUV-001",
        DataViagem = DateTime.UtcNow,
        NavioId = 10
    };
}
