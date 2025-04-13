namespace Domain.Stripe;
public class CheckoutResponse
{
    public string? SessionId { get; set; }
    public string? PublicKey { get; set; }
}
