using SharedKernel;

namespace Domain.Products;
public sealed class ProductPrice : Entity
{
    public string Id { get; set; }
    public bool Active { get; set; }
    public string BillingScheme { get; set; }
    public string Currency { get; set; }
    public bool Livemode { get; set; }
    public string ProductId { get; set; }
    public long? UnitAmount { get; set; }
    public decimal? UnitAmountDecimal { get; set; }

    //Navigation Properties
    public Product Product { get; set; }
}
