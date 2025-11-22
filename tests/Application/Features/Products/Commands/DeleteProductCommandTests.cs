using Application.Features.Products.Commands;
using Application.Interfaces;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Tests.Application.Features.Products.Commands;

public class DeleteProductCommandTests
{
    private readonly Mock<IRepository<Product>> _repositoryMock;
    private readonly DeleteProductCommandHandler _handler;

    public DeleteProductCommandTests()
    {
        _repositoryMock = new Mock<IRepository<Product>>();
        _handler = new DeleteProductCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenProductExists_ShouldDeleteAndReturnTrue()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var existingProduct = new Product { Id = productId, Name = "Test", Price = 50m };
        var command = new DeleteProductCommand(productId);

        _repositoryMock
            .Setup(r => r.GetByIdAsync(productId))
            .ReturnsAsync(existingProduct);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        _repositoryMock.Verify(r => r.DeleteAsync(existingProduct), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenProductDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var command = new DeleteProductCommand(productId);

        _repositoryMock
            .Setup(r => r.GetByIdAsync(productId))
            .ReturnsAsync((Product?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Product>()), Times.Never);
    }
}
