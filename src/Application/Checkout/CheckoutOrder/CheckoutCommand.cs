using Application.Abstractions.Messaging;
using Domain.Stripe;

namespace Application.Stripe.CheckoutOrder;

public sealed class CheckoutCommand : ICommand<string>
{ 
    public string Currency { get; set; }
    public List<OrderItem> Items { get; set; }
}

