using Moq;
using Seals.Duv.Application.Constants;
using Seals.Duv.Application.Services;
using Seals.Duv.Domain.Entities;
using Seals.Duv.Domain.Interfaces;
using ValidationException = Seals.Duv.Application.Exceptions.ValidationException;

namespace Seals.Duv.Tests.Application.Services;

public class NavioServiceTests
{
    private readonly Mock<INavioRepository> _repositoryMock = new();
    private readonly Mock<IValidator<Navio>> _validatorMock = new();
    private readonly NavioService _service;

    public NavioServiceTests()
    {
        _service = new NavioService(_repositoryMock.Object, _validatorMock.Object);
    }

    [Fact(DisplayName = "Should return all ships")]
    public async Task GetAllAsync_ShouldReturnNavios()
    {
        // Arrange
        var navios = new List<Navio> { CreateValidNavio() };
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(navios);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact(DisplayName = "Should return ship by Guid")]
    public async Task GetByGuidAsync_ShouldReturnNavio()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var navio = CreateValidNavio();
        _repositoryMock.Setup(r => r.GetByGuidAsync(guid)).ReturnsAsync(navio);

        // Act
        var result = await _service.GetByGuidAsync(guid);

        // Assert
        Assert.NotNull(result);
    }

    [Fact(DisplayName = "Should validate and create ship")]
    public async Task CreateAsync_ShouldValidateAndCreateNavio()
    {
        // Arrange
        var navio = CreateValidNavio();
        _repositoryMock.Setup(r => r.CreateAsync(navio)).ReturnsAsync(navio);

        // Act
        var result = await _service.CreateAsync(navio);

        // Assert
        _validatorMock.Verify(v => v.Validate(navio), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(navio), Times.Once);
        Assert.Equal(navio, result);
    }

    [Fact(DisplayName = "Should throw ValidationException with correct messages")]
    public async Task CreateAsync_ShouldThrowValidationException_WithCorrectMessages()
    {
        // Arrange
        var navio = CreateValidNavio();

        var errors = new List<string>
        {
            ValidationMessages.NavioNomeObrigatorio,
            ValidationMessages.NavioBandeiraObrigatoria,
            ValidationMessages.NavioImagemObrigatoria
        };

        _validatorMock
            .Setup(v => v.Validate(It.IsAny<Navio>()))
            .Throws(new ValidationException(errors));

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ValidationException>(() => _service.CreateAsync(navio));
        Assert.Equal(3, ex.Errors.Count());
        Assert.Contains(ValidationMessages.NavioNomeObrigatorio, ex.Errors);
        Assert.Contains(ValidationMessages.NavioBandeiraObrigatoria, ex.Errors);
        Assert.Contains(ValidationMessages.NavioImagemObrigatoria, ex.Errors);

        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Navio>()), Times.Never);
    }

    [Fact(DisplayName = "Should update ship if it exists")]
    public async Task UpdateByGuidAsync_ShouldUpdateNavio_IfExists()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var existing = CreateValidNavio();
        var updated = CreateValidNavio();
        updated.Nome = "Navio Atualizado";

        _repositoryMock.Setup(r => r.GetByGuidAsync(guid)).ReturnsAsync(existing);

        // Act
        await _service.UpdateByGuidAsync(guid, updated);

        // Assert
        _validatorMock.Verify(v => v.Validate(It.Is<Navio>(n => n.Nome == "Navio Atualizado")), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.Is<Navio>(n => n.Nome == "Navio Atualizado")), Times.Once);
    }

    [Fact(DisplayName = "Should not update ship if not found")]
    public async Task UpdateByGuidAsync_ShouldDoNothing_IfNotFound()
    {
        // Arrange
        var guid = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByGuidAsync(guid)).ReturnsAsync((Navio?)null);

        // Act
        await _service.UpdateByGuidAsync(guid, CreateValidNavio());

        // Assert
        _validatorMock.Verify(v => v.Validate(It.IsAny<Navio>()), Times.Never);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Navio>()), Times.Never);
    }

    [Fact(DisplayName = "Should delete ship if found")]
    public async Task DeleteByGuidAsync_ShouldDeleteNavio_IfFound()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var navio = CreateValidNavio();
        _repositoryMock.Setup(r => r.GetByGuidAsync(guid)).ReturnsAsync(navio);

        // Act
        await _service.DeleteByGuidAsync(guid);

        // Assert
        _repositoryMock.Verify(r => r.DeleteAsync(navio), Times.Once);
    }

    [Fact(DisplayName = "Should not delete ship if not found")]
    public async Task DeleteByGuidAsync_ShouldDoNothing_IfNotFound()
    {
        // Arrange
        var guid = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByGuidAsync(guid)).ReturnsAsync((Navio?)null);

        // Act
        await _service.DeleteByGuidAsync(guid);

        // Assert
        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Navio>()), Times.Never);
    }

    private static Navio CreateValidNavio() => new()
    {
        Id = 1,
        Nome = "Navio Azul",
        Bandeira = "Brasil",
        ImagemUrl = "https://navio.jpg"
    };
}
