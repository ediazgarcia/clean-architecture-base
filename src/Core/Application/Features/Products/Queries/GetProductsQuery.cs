using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Queries;

public record GetProductsQuery : IRequest<List<Product>>;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
{
    private readonly IRepository<Product> _repository;

    public GetProductsQueryHandler(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
