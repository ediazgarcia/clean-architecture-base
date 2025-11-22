using Application.Features.Products.Commands;
using Application.Interfaces;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Tests.Application.Features.Products.Commands;

public class UpdateProductCommandTests
{
    private readonly Mock<IRepository<Product>> _repositoryMock;
    private readonly UpdateProductCommandHandler _handler;

    public UpdateProductCommandTests()
    {
        _repositoryMock = new Mock<IRepository<Product>>();
        _handler = new UpdateProductCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenProductExists_ShouldUpdateAndReturnTrue()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var existingProduct = new Product { Id = productId, Name = "Old Name", Price = 50m };
        var command = new UpdateProductCommand(productId, "New Name", 100m);

        _repositoryMock
            .Setup(r => r.GetByIdAsync(productId))
            .ReturnsAsync(existingProduct);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        existingProduct.Name.Should().Be("New Name");
        existingProduct.Price.Should().Be(100m);
        _repositoryMock.Verify(r => r.UpdateAsync(existingProduct), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenProductDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var command = new UpdateProductCommand(productId, "New Name", 100m);

        _repositoryMock
            .Setup(r => r.GetByIdAsync(productId))
            .ReturnsAsync((Product?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Never);
    }
}
