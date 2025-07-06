using Moq;
using Seals.Duv.Application.Constants;
using Seals.Duv.Application.Services;
using Seals.Duv.Domain.Interfaces;
using ValidationException = Seals.Duv.Application.Exceptions.ValidationException;

namespace Seals.Duv.Tests.Application.Services;

public class DuvServiceTests
{
    private readonly Mock<IDuvRepository> _repositoryMock = new();
    private readonly Mock<IValidator<Domain.Entities.Duv>> _validatorMock = new();
    private readonly DuvService _service;

    public DuvServiceTests()
    {
        _service = new DuvService(_repositoryMock.Object, _validatorMock.Object);
    }

    [Fact(DisplayName = "Should return all DUVs")]
    public async Task GetAllAsync_ShouldReturnDuvs()
    {
        // Arrange
        var duvs = new List<Domain.Entities.Duv> { CreateValidDuv() };
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(duvs);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact(DisplayName = "Should return DUV by Guid")]
    public async Task GetByGuidAsync_ShouldReturnDuv()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var duv = CreateValidDuv();
        _repositoryMock.Setup(r => r.GetByGuidAsync(guid)).ReturnsAsync(duv);

        // Act
        var result = await _service.GetByGuidAsync(guid);

        // Assert
        Assert.NotNull(result);
    }

    [Fact(DisplayName = "Should validate and create DUV")]
    public async Task CreateAsync_ShouldValidateAndCreateDuv()
    {
        // Arrange
        var duv = CreateValidDuv();
        _repositoryMock.Setup(r => r.CreateAsync(duv)).ReturnsAsync(duv);

        // Act
        var result = await _service.CreateAsync(duv);

        // Assert
        _validatorMock.Verify(v => v.Validate(duv), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(duv), Times.Once);
        Assert.Equal(duv, result);
    }

    [Fact(DisplayName = "Should throw ValidationException with correct messages")]
    public async Task CreateAsync_ShouldThrowValidationException_WithCorrectMessages()
    {
        // Arrange
        var duv = CreateValidDuv();
        var errors = new List<string>
        {
            ValidationMessages.DuvNumeroObrigatorio,
            ValidationMessages.DuvDataObrigatoria,
            ValidationMessages.DuvNavioIdInvalido
        };

        _validatorMock.Setup(v => v.Validate(It.IsAny<Domain.Entities.Duv>())).Throws(new ValidationException(errors));

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ValidationException>(() => _service.CreateAsync(duv));
        Assert.Equal(3, ex.Errors.Count());
        Assert.Contains(ValidationMessages.DuvNumeroObrigatorio, ex.Errors);
        Assert.Contains(ValidationMessages.DuvDataObrigatoria, ex.Errors);
        Assert.Contains(ValidationMessages.DuvNavioIdInvalido, ex.Errors);

        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Domain.Entities.Duv>()), Times.Never);
    }

    [Fact(DisplayName = "Should update DUV if it exists")]
    public async Task UpdateByGuidAsync_ShouldUpdateDuv_IfExists()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var existing = CreateValidDuv();
        var updated = CreateValidDuv();
        updated.Numero = "DUV-UPDATED";

        _repositoryMock.Setup(r => r.GetByGuidAsync(guid)).ReturnsAsync(existing);

        // Act
        await _service.UpdateByGuidAsync(guid, updated);

        // Assert
        _validatorMock.Verify(v => v.Validate(It.Is<Domain.Entities.Duv>(d => d.Numero == "DUV-UPDATED")), Times.Once);
        _repositoryMock.Verify(r => r.UpdateAsync(It.Is<Domain.Entities.Duv>(d => d.Numero == "DUV-UPDATED")), Times.Once);
    }

    [Fact(DisplayName = "Should not update DUV if not found")]
    public async Task UpdateByGuidAsync_ShouldDoNothing_IfNotFound()
    {
        // Arrange
        var guid = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByGuidAsync(guid)).ReturnsAsync((Domain.Entities.Duv?)null);

        // Act
        await _service.UpdateByGuidAsync(guid, CreateValidDuv());

        // Assert
        _validatorMock.Verify(v => v.Validate(It.IsAny<Domain.Entities.Duv>()), Times.Never);
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Domain.Entities.Duv>()), Times.Never);
    }

    [Fact(DisplayName = "Should delete DUV if found")]
    public async Task DeleteByGuidAsync_ShouldDeleteDuv_IfFound()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var duv = CreateValidDuv();
        _repositoryMock.Setup(r => r.GetByGuidAsync(guid)).ReturnsAsync(duv);

        // Act
        await _service.DeleteByGuidAsync(guid);

        // Assert
        _repositoryMock.Verify(r => r.DeleteAsync(duv), Times.Once);
    }

    [Fact(DisplayName = "Should not delete DUV if not found")]
    public async Task DeleteByGuidAsync_ShouldDoNothing_IfNotFound()
    {
        // Arrange
        var guid = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByGuidAsync(guid)).ReturnsAsync((Domain.Entities.Duv?)null);

        // Act
        await _service.DeleteByGuidAsync(guid);

        // Assert
        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Domain.Entities.Duv>()), Times.Never);
    }

    private static Domain.Entities.Duv CreateValidDuv() => new()
    {
        Id = 1,
        Numero = "DUV001",
        DataViagem = DateTime.UtcNow,
        NavioId = 1
    };
}
