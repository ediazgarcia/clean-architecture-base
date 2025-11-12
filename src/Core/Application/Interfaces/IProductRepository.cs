namespace Application.Interfaces;
using Shared.DTOs;

public interface IProductRepository
{
    Task<IEnumerable<ProductDto>> GetAllsAsync();
    Task AddAsync(ProductDto product);

}