namespace Domain.Stripe.Checkout;

public class CheckoutCompletedNotification
{
    public string SessionId { get; set; } = string.Empty; // Stripe session ID
    public string Currency { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public DateTime CheckoutDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public string PaymentStatus { get; set; } = string.Empty; // completed, pending, failed
    public string? PaymentIntentId { get; set; }
}
