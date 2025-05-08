namespace Domain.Stripe.Checkout;
public class CheckoutResponse
{
    public string? SessionId { get; set; }
    public string? PublicKey { get; set; }
}
