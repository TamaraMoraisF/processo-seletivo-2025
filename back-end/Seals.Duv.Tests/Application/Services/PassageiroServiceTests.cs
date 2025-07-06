using Moq;
using Seals.Duv.Application.Constants;
using Seals.Duv.Application.Services;
using Seals.Duv.Domain.Entities;
using Seals.Duv.Domain.Enum;
using Seals.Duv.Domain.Interfaces;
using ValidationException = Seals.Duv.Application.Exceptions.ValidationException;

namespace Seals.Duv.Tests.Application.Services;

public class PassageiroServiceTests
{
    private readonly Mock<IPassageiroRepository> _repositoryMock = new();
    private readonly Mock<IValidator<Passageiro>> _validatorMock = new();
    private readonly PassageiroService _service;

    public PassageiroServiceTests()
    {
        _service = new PassageiroService(_repositoryMock.Object, _validatorMock.Object);
    }

    [Fact(DisplayName = "Should return all passengers")]
    public async Task GetAllAsync_ShouldReturnPassengers()
    {
        // Arrange
        var passengers = new List<Passageiro> { CreateValidPassenger() };
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(passengers);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact(DisplayName = "Should return passenger by Guid")]
    public async Task GetByGuidAsync_ShouldReturnPassenger()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var passenger = CreateValidPassenger();
        _repositoryMock.Setup(r => r.GetByGuidAsync(guid)).ReturnsAsync(passenger);

        // Act
        var result = await _service.GetByGuidAsync(guid);

        // Assert
        Assert.NotNull(result);
    }

    [Fact(DisplayName = "Should validate and add passenger")]
    public async Task CreateAsync_ShouldValidateAndAddPassenger()
    {
        // Arrange
        var passenger = CreateValidPassenger();

        // Act
        var result = await _service.CreateAsync(passenger);

        // Assert
        _validatorMock.Verify(v => v.Validate(passenger), Times.Once);
        _repositoryMock.Verify(r => r.AddAsync(passenger), Times.Once);
        Assert.Equal(passenger, result);
    }

    [Fact(DisplayName = "Should throw ValidationException with correct messages")]
    public async Task CreateAsync_ShouldThrowValidationException_WithCorrectMessages()
    {
        // Arrange
        var passenger = CreateValidPassenger();
        var errors = new List<string>
        {
            ValidationMessages.PassageiroNomeObrigatorio,
            ValidationMessages.PassageiroFotoObrigatoria,
            ValidationMessages.PassageiroDuvIdInvalido
        };

        _validatorMock
            .Setup(v => v.Validate(It.IsAny<Passageiro>()))
            .Throws(new ValidationException(errors));

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ValidationException>(() => _service.CreateAsync(passenger));
        Assert.Equal(3, ex.Errors.Count());
        Assert.Contains(ValidationMessages.PassageiroNomeObrigatorio, ex.Errors);
        Assert.Contains(ValidationMessages.PassageiroFotoObrigatoria, ex.Errors);
        Assert.Contains(ValidationMessages.PassageiroDuvIdInvalido, ex.Errors);

        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Passageiro>()), Times.Never);
    }

    [Fact(DisplayName = "Should update passenger if it exists")]
    public async Task UpdateByGuidAsync_ShouldUpdatePassenger_IfExists()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var existing = CreateValidPassenger();
        var updated = CreateValidPassenger();
        updated.Nome = "Updated";

        _repositoryMock.Setup(r => r.GetByGuidAsync(guid)).ReturnsAsync(existing);

        // Act
        await _service.UpdateByGuidAsync(guid, updated);

        // Assert
        _validatorMock.Verify(v => v.Validate(It.Is<Passageiro>(p => p.Nome == "Updated")), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.Is<Passageiro>(p => p.Nome == "Updated")), Times.Once);
    }

    [Fact(DisplayName = "Should not update passenger if not found")]
    public async Task UpdateByGuidAsync_ShouldDoNothing_IfNotFound()
    {
        // Arrange
        var guid = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByGuidAsync(guid)).ReturnsAsync((Passageiro?)null);

        // Act
        await _service.UpdateByGuidAsync(guid, CreateValidPassenger());

        // Assert
        _validatorMock.Verify(v => v.Validate(It.IsAny<Passageiro>()), Times.Never);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Passageiro>()), Times.Never);
    }

    [Fact(DisplayName = "Should delete passenger if found")]
    public async Task DeleteByGuidAsync_ShouldDeletePassenger_IfFound()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var passenger = CreateValidPassenger();
        _repositoryMock.Setup(r => r.GetByGuidAsync(guid)).ReturnsAsync(passenger);

        // Act
        await _service.DeleteByGuidAsync(guid);

        // Assert
        _repositoryMock.Verify(r => r.DeleteAsync(passenger), Times.Once);
    }

    [Fact(DisplayName = "Should not delete passenger if not found")]
    public async Task DeleteByGuidAsync_ShouldDoNothing_IfNotFound()
    {
        // Arrange
        var guid = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByGuidAsync(guid)).ReturnsAsync((Passageiro?)null);

        // Act
        await _service.DeleteByGuidAsync(guid);

        // Assert
        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Passageiro>()), Times.Never);
    }

    private static Passageiro CreateValidPassenger() => new()
    {
        Id = 1,
        Nome = "João",
        Nacionalidade = "Brasileira",
        FotoUrl = "foto.jpg",
        Tipo = TipoPassageiro.Passageiro,
        DuvId = 2,
        Sid = null
    };
}
