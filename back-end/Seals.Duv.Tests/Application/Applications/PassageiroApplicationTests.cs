using AutoMapper;
using Moq;
using Seals.Duv.Application.Applications;
using Seals.Duv.Application.DTOs;
using Seals.Duv.Domain.Entities;
using Seals.Duv.Domain.Interfaces;

namespace Seals.Duv.Tests.Application.Applications;

public class PassageiroApplicationTests
{
    private readonly Mock<IPassageiroService> _passageiroServiceMock = new();
    private readonly Mock<IDuvService> _duvServiceMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly PassageiroApplication _application;

    public PassageiroApplicationTests()
    {
        _application = new PassageiroApplication(
            _passageiroServiceMock.Object,
            _duvServiceMock.Object,
            _mapperMock.Object
        );
    }

    [Fact(DisplayName = "Should return mapped list of passengers")]
    public async Task GetAllAsync_ShouldReturnMappedList()
    {
        // Arrange
        var passageiros = new List<Passageiro> { CreatePassageiro() };
        var dtos = new List<PassageiroDto> { new() { Nome = "João" } };

        _passageiroServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(passageiros);
        _mapperMock.Setup(m => m.Map<IEnumerable<PassageiroDto>>(passageiros)).Returns(dtos);

        // Act
        var result = await _application.GetAllAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("João", result.First().Nome);
    }

    [Fact(DisplayName = "Should return passenger by Guid")]
    public async Task GetByGuidAsync_ShouldReturnMappedPassenger()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var entity = CreatePassageiro();
        var dto = new PassageiroDto { Nome = "João" };

        _passageiroServiceMock.Setup(s => s.GetByGuidAsync(guid)).ReturnsAsync(entity);
        _mapperMock.Setup(m => m.Map<PassageiroDto>(entity)).Returns(dto);

        // Act
        var result = await _application.GetByGuidAsync(guid);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("João", result?.Nome);
    }

    [Fact(DisplayName = "Should return null if DUV not found when creating")]
    public async Task CreateAsync_ShouldReturnNull_IfDuvNotFound()
    {
        // Arrange
        var dto = new CreatePassageiroDto { DuvGuid = Guid.NewGuid() };
        _duvServiceMock.Setup(d => d.GetByGuidAsync(dto.DuvGuid)).ReturnsAsync((Domain.Entities.Duv?)null);

        // Act
        var result = await _application.CreateAsync(dto);

        // Assert
        Assert.Null(result);
    }

    [Fact(DisplayName = "Should create passenger if DUV exists")]
    public async Task CreateAsync_ShouldCreatePassenger_IfDuvExists()
    {
        // Arrange
        var dto = new CreatePassageiroDto
        {
            Nome = "João",
            Nacionalidade = "Brasileira",
            FotoUrl = "foto.jpg",
            Tipo = Seals.Duv.Domain.Enum.TipoPassageiro.Passageiro,
            DuvGuid = Guid.NewGuid()
        };

        var duv = CreateDuv();
        var entity = CreatePassageiro();
        var created = CreatePassageiro();
        var expectedDto = new PassageiroDto { Nome = "João" };

        _duvServiceMock.Setup(d => d.GetByGuidAsync(dto.DuvGuid)).ReturnsAsync(duv);
        _mapperMock.Setup(m => m.Map<Passageiro>(dto)).Returns(entity);
        _passageiroServiceMock.Setup(s => s.CreateAsync(entity)).ReturnsAsync(created);
        _mapperMock.Setup(m => m.Map<PassageiroDto>(created)).Returns(expectedDto);

        // Act
        var result = await _application.CreateAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("João", result?.Nome);
    }

    [Fact(DisplayName = "Should return false if DUV not found on update")]
    public async Task UpdateByGuidAsync_ShouldReturnFalse_IfDuvNotFound()
    {
        // Arrange
        var dto = new UpdatePassageiroDto { DuvGuid = Guid.NewGuid() };
        _duvServiceMock.Setup(d => d.GetByGuidAsync(dto.DuvGuid)).ReturnsAsync((Domain.Entities.Duv?)null);

        // Act
        var result = await _application.UpdateByGuidAsync(Guid.NewGuid(), dto);

        // Assert
        Assert.False(result);
    }

    [Fact(DisplayName = "Should update passenger if DUV exists")]
    public async Task UpdateByGuidAsync_ShouldUpdatePassenger_IfDuvExists()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var dto = new UpdatePassageiroDto
        {
            Nome = "Atualizado",
            Nacionalidade = "Brasil",
            FotoUrl = "url",
            Tipo = Seals.Duv.Domain.Enum.TipoPassageiro.Passageiro,
            DuvGuid = Guid.NewGuid()
        };

        var duv = CreateDuv();
        var entity = CreatePassageiro();

        _duvServiceMock.Setup(d => d.GetByGuidAsync(dto.DuvGuid)).ReturnsAsync(duv);
        _mapperMock.Setup(m => m.Map<Passageiro>(dto)).Returns(entity);

        // Act
        var result = await _application.UpdateByGuidAsync(guid, dto);

        // Assert
        Assert.True(result);
        _passageiroServiceMock.Verify(s => s.UpdateByGuidAsync(guid, entity), Times.Once);
    }

    [Fact(DisplayName = "Should delete passenger by Guid")]
    public async Task DeleteByGuidAsync_ShouldCallService()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        await _application.DeleteByGuidAsync(guid);

        // Assert
        _passageiroServiceMock.Verify(s => s.DeleteByGuidAsync(guid), Times.Once);
    }

    private static Passageiro CreatePassageiro(int id = 1) => new()
    {
        Id = id,
        Nome = "João",
        Nacionalidade = "Brasileira",
        FotoUrl = "foto.jpg",
        Tipo = Domain.Enum.TipoPassageiro.Passageiro,
        DuvId = 99
    };

    private static Domain.Entities.Duv CreateDuv(int id = 99) => new()
    {
        Id = id,
        Numero = "DUV001",
        DataViagem = DateTime.UtcNow,
        NavioId = 1
    };
}
