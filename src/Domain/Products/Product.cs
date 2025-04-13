using SharedKernel;

namespace Domain.Product;
public sealed class Product : Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long Price { get; set; }
    public string PriceId { get; set; }
    public long Updated { get; set; }
    public bool Active { get; set; }
    public List<string> Images { get; set; } = new();
}
