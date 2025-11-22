using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands;

public record DeleteProductCommand(Guid Id) : IRequest<bool>;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IRepository<Product> _repository;

    public DeleteProductCommandHandler(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id);
        if (product == null)
        {
            return false;
        }

        await _repository.DeleteAsync(product);
        return true;
    }
}
