using AutoMapper;
using Moq;
using Seals.Duv.Application.Applications;
using Seals.Duv.Application.DTOs;
using Seals.Duv.Domain.Entities;
using Seals.Duv.Domain.Interfaces;

namespace Seals.Duv.Tests.Application.Applications;

public class NavioApplicationTests
{
    private readonly Mock<INavioService> _serviceMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly NavioApplication _application;

    public NavioApplicationTests()
    {
        _application = new NavioApplication(_serviceMock.Object, _mapperMock.Object);
    }

    [Fact(DisplayName = "Should return mapped list of ships")]
    public async Task GetAllAsync_ShouldReturnMappedList()
    {
        // Arrange
        var navios = new List<Navio> { CreateNavio() };
        var dtos = new List<NavioDto> { new() { Nome = "Navio Teste" } };

        _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(navios);
        _mapperMock.Setup(m => m.Map<IEnumerable<NavioDto>>(navios)).Returns(dtos);

        // Act
        var result = await _application.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Navio Teste", result.First().Nome);
    }

    [Fact(DisplayName = "Should return mapped ship by Guid")]
    public async Task GetByGuidAsync_ShouldReturnMappedShip()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var navio = CreateNavio();
        var dto = new NavioDto { Nome = "Navio Teste" };

        _serviceMock.Setup(s => s.GetByGuidAsync(guid)).ReturnsAsync(navio);
        _mapperMock.Setup(m => m.Map<NavioDto>(navio)).Returns(dto);

        // Act
        var result = await _application.GetByGuidAsync(guid);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Navio Teste", result?.Nome);
    }

    [Fact(DisplayName = "Should create ship and return mapped DTO")]
    public async Task CreateAsync_ShouldCreateAndReturnDto()
    {
        // Arrange
        var dto = new CreateNavioDto { Nome = "Navio", Bandeira = "BR", ImagemUrl = "url" };
        var entity = CreateNavio();
        var created = CreateNavio();
        var expectedDto = new NavioDto { Nome = "Navio" };

        _mapperMock.Setup(m => m.Map<Navio>(dto)).Returns(entity);
        _serviceMock.Setup(s => s.CreateAsync(entity)).ReturnsAsync(created);
        _mapperMock.Setup(m => m.Map<NavioDto>(created)).Returns(expectedDto);

        // Act
        var result = await _application.CreateAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Navio", result.Nome);
    }

    [Fact(DisplayName = "Should update ship by Guid")]
    public async Task UpdateByGuidAsync_ShouldUpdateEntity()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var dto = new UpdateNavioDto { Nome = "Atualizado", Bandeira = "BR", ImagemUrl = "img" };
        var entity = CreateNavio();

        _mapperMock.Setup(m => m.Map<Navio>(dto)).Returns(entity);

        // Act
        await _application.UpdateByGuidAsync(guid, dto);

        // Assert
        _serviceMock.Verify(s => s.UpdateByGuidAsync(guid, entity), Times.Once);
    }

    [Fact(DisplayName = "Should delete ship by Guid")]
    public async Task DeleteByGuidAsync_ShouldCallService()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        await _application.DeleteByGuidAsync(guid);

        // Assert
        _serviceMock.Verify(s => s.DeleteByGuidAsync(guid), Times.Once);
    }

    private static Navio CreateNavio() => new()
    {
        Id = 1,
        Nome = "Navio Teste",
        Bandeira = "Brasil",
        ImagemUrl = "http://imagem.com/navio.jpg"
    };
}
