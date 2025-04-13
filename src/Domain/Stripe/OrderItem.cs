namespace Domain.Stripe;
public sealed class OrderItem
{
    public string ItemId { get; set; }
    public string PriceId { get; set; }
    public int Quantity { get; set; }
}
