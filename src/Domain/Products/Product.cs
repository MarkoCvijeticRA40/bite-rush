using SharedKernel;

namespace Domain.Products;
public sealed class Product : Entity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long Updated { get; set; }
    public bool Active { get; set; }
    public bool LiveMode { get; set; }
    public List<string> Images { get; set; } = new();

    //Navigation Properties
    public ICollection<ProductPrice> Prices { get; set; }
}
