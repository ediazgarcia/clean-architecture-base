using Domain.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}