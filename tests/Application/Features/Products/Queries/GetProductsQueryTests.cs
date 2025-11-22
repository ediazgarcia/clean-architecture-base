using Application.Features.Products.Queries;
using Application.Interfaces;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Tests.Application.Features.Products.Queries;

public class GetProductsQueryTests
{
    private readonly Mock<IRepository<Product>> _repositoryMock;
    private readonly GetProductsQueryHandler _handler;

    public GetProductsQueryTests()
    {
        _repositoryMock = new Mock<IRepository<Product>>();
        _handler = new GetProductsQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Product 1", Price = 10m },
            new Product { Id = Guid.NewGuid(), Name = "Product 2", Price = 20m }
        };

        _repositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(products);

        var query = new GetProductsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(products);
    }

    [Fact]
    public async Task Handle_WhenNoProducts_ShouldReturnEmptyList()
    {
        // Arrange
        _repositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<Product>());

        var query = new GetProductsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }
}
