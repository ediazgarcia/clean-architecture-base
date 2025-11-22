using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<Product?>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product?>
{
    private readonly IRepository<Product> _repository;

    public GetProductByIdQueryHandler(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
