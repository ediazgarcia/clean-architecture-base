using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands;

public record UpdateProductCommand(Guid Id, string Name, decimal Price) : IRequest<bool>;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IRepository<Product> _repository;

    public UpdateProductCommandHandler(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id);
        if (product == null)
        {
            return false;
        }

        product.Name = request.Name;
        product.Price = request.Price;

        await _repository.UpdateAsync(product);
        return true;
    }
}
