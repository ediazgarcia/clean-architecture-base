using Application.Features.Products.Commands;
using Application.Interfaces;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Tests.Application.Features.Products.Commands;

public class CreateProductCommandTests
{
    private readonly Mock<IRepository<Product>> _repositoryMock;
    private readonly CreateProductCommandHandler _handler;

    public CreateProductCommandTests()
    {
        _repositoryMock = new Mock<IRepository<Product>>();
        _handler = new CreateProductCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateProduct_AndReturnId()
    {
        // Arrange
        var command = new CreateProductCommand("Test Product", 99.99m);
        var expectedId = Guid.NewGuid();

        _repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Product>()))
            .ReturnsAsync((Product p) =>
            {
                p.Id = expectedId;
                return p;
            });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(expectedId);
        _repositoryMock.Verify(r => r.AddAsync(It.Is<Product>(p =>
            p.Name == "Test Product" &&
            p.Price == 99.99m
        )), Times.Once);
    }
}
