using AutoMapper;
using Moq;
using Seals.Duv.Application.Applications;
using Seals.Duv.Application.DTOs;
using Seals.Duv.Domain.Entities;
using Seals.Duv.Domain.Interfaces;

namespace Seals.Duv.Tests.Application.Applications;

public class DuvApplicationTests
{
    private readonly Mock<IDuvService> _duvServiceMock = new();
    private readonly Mock<INavioService> _navioServiceMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly DuvApplication _application;

    public DuvApplicationTests()
    {
        _application = new DuvApplication(
            _duvServiceMock.Object,
            _navioServiceMock.Object,
            _mapperMock.Object
        );
    }

    [Fact(DisplayName = "Should return all mapped DUVs")]
    public async Task GetAllAsync_ShouldReturnMappedDuvs()
    {
        // Arrange
        var duvs = new List<Domain.Entities.Duv> { CreateDuv() };
        var dtos = new List<DuvDto> { new() { Numero = "DUV123" } };

        _duvServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(duvs);
        _mapperMock.Setup(m => m.Map<IEnumerable<DuvDto>>(duvs)).Returns(dtos);

        // Act
        var result = await _application.GetAllAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("DUV123", result.First().Numero);
    }

    [Fact(DisplayName = "Should return mapped DUV by Guid")]
    public async Task GetByGuidAsync_ShouldReturnMappedDuv()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var duv = CreateDuv();
        var dto = new DuvDto { Numero = "DUV123" };

        _duvServiceMock.Setup(s => s.GetByGuidAsync(guid)).ReturnsAsync(duv);
        _mapperMock.Setup(m => m.Map<DuvDto?>(duv)).Returns(dto);

        // Act
        var result = await _application.GetByGuidAsync(guid);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("DUV123", result?.Numero);
    }

    [Fact(DisplayName = "Should return null if navio not found on create")]
    public async Task CreateAsync_ShouldReturnNull_IfNavioNotFound()
    {
        // Arrange
        var dto = new CreateDuvDto { NavioGuid = Guid.NewGuid() };
        _navioServiceMock.Setup(n => n.GetByGuidAsync(dto.NavioGuid)).ReturnsAsync((Navio?)null);

        // Act
        var result = await _application.CreateAsync(dto);

        // Assert
        Assert.Null(result);
    }

    [Fact(DisplayName = "Should create DUV and return mapped DTO")]
    public async Task CreateAsync_ShouldCreateDuv_IfNavioExists()
    {
        // Arrange
        var dto = new CreateDuvDto
        {
            Numero = "DUV123",
            DataViagem = DateTime.UtcNow,
            NavioGuid = Guid.NewGuid()
        };

        var navio = CreateNavio();
        var duvEntity = CreateDuv();
        var created = CreateDuv();
        var expectedDto = new DuvDto { Numero = "DUV123" };

        _navioServiceMock.Setup(n => n.GetByGuidAsync(dto.NavioGuid)).ReturnsAsync(navio);
        _mapperMock.Setup(m => m.Map<Domain.Entities.Duv>(dto)).Returns(duvEntity);
        _duvServiceMock.Setup(s => s.CreateAsync(duvEntity)).ReturnsAsync(created);
        _mapperMock.Setup(m => m.Map<DuvDto>(created)).Returns(expectedDto);

        // Act
        var result = await _application.CreateAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("DUV123", result?.Numero);
    }

    [Fact(DisplayName = "Should return false if navio not found on update")]
    public async Task UpdateByGuidAsync_ShouldReturnFalse_IfNavioNotFound()
    {
        // Arrange
        var dto = new UpdateDuvDto { NavioGuid = Guid.NewGuid() };
        _navioServiceMock.Setup(n => n.GetByGuidAsync(dto.NavioGuid)).ReturnsAsync((Navio?)null);

        // Act
        var result = await _application.UpdateByGuidAsync(Guid.NewGuid(), dto);

        // Assert
        Assert.False(result);
    }

    [Fact(DisplayName = "Should update DUV if navio found")]
    public async Task UpdateByGuidAsync_ShouldUpdateDuv_IfNavioExists()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var dto = new UpdateDuvDto
        {
            Numero = "DUV456",
            DataViagem = DateTime.UtcNow,
            NavioGuid = Guid.NewGuid()
        };

        var navio = CreateNavio();
        var entity = CreateDuv();

        _navioServiceMock.Setup(n => n.GetByGuidAsync(dto.NavioGuid)).ReturnsAsync(navio);
        _mapperMock.Setup(m => m.Map<Domain.Entities.Duv>(dto)).Returns(entity);

        // Act
        var result = await _application.UpdateByGuidAsync(guid, dto);

        // Assert
        Assert.True(result);
        _duvServiceMock.Verify(s => s.UpdateByGuidAsync(guid, entity), Times.Once);
    }

    [Fact(DisplayName = "Should delete DUV by Guid")]
    public async Task DeleteByGuidAsync_ShouldCallService()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        await _application.DeleteByGuidAsync(guid);

        // Assert
        _duvServiceMock.Verify(s => s.DeleteByGuidAsync(guid), Times.Once);
    }

    private static Domain.Entities.Duv CreateDuv() => new()
    {
        Id = 1,
        Numero = "DUV123",
        DataViagem = DateTime.UtcNow,
        NavioId = 5
    };

    private static Navio CreateNavio() => new()
    {
        Id = 5,
        Nome = "Navio Teste",
        Bandeira = "Brasil",
        ImagemUrl = "http://imagem.jpg"
    };
}
