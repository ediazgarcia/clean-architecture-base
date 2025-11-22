using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands;

public record CreateProductCommand(string Name, decimal Price) : IRequest<Guid>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IRepository<Product> _repository;

    public CreateProductCommandHandler(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price
        };

        await _repository.AddAsync(product);

        return product.Id;
    }
}
