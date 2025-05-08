using Application.Abstractions.Messaging;
using Domain.Stripe.Checkout;

namespace Application.Stripe.Checkout;

public sealed class CheckoutCommand : ICommand<string>
{
    public string Currency { get; set; }
    public List<OrderItem> Items { get; set; }
}

