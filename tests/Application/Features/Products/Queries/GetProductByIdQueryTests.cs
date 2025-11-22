using Application.Features.Products.Queries;
using Application.Interfaces;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Tests.Application.Features.Products.Queries;

public class GetProductByIdQueryTests
{
    private readonly Mock<IRepository<Product>> _repositoryMock;
    private readonly GetProductByIdQueryHandler _handler;

    public GetProductByIdQueryTests()
    {
        _repositoryMock = new Mock<IRepository<Product>>();
        _handler = new GetProductByIdQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenProductExists_ShouldReturnProduct()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new Product { Id = productId, Name = "Test Product", Price = 50m };

        _repositoryMock
            .Setup(r => r.GetByIdAsync(productId))
            .ReturnsAsync(product);

        var query = new GetProductByIdQuery(productId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(product);
    }

    [Fact]
    public async Task Handle_WhenProductDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var productId = Guid.NewGuid();

        _repositoryMock
            .Setup(r => r.GetByIdAsync(productId))
            .ReturnsAsync((Product?)null);

        var query = new GetProductByIdQuery(productId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
}
