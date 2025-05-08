using Application.Abstractions.Messaging;
using Domain.Stripe.Checkout;
using Microsoft.Extensions.Configuration;
using SharedKernel;
using Stripe.Checkout;

namespace Application.Stripe.Checkout;
internal sealed class CheckoutCommandHandler(IConfiguration configuration) : ICommandHandler<CheckoutCommand, string>
{
    public async Task<Result<string>> Handle(CheckoutCommand command, CancellationToken cancellationToken)
    {
        var lineItems = new List<SessionLineItemOptions>();

        foreach (OrderItem item in command.Items)
        {
            lineItems.Add(new SessionLineItemOptions
            {
                Price = item.PriceId,
                Quantity = item.Quantity,
            });
        }

        var options = new SessionCreateOptions
        {
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = configuration["Frontend:Payment:PaymentSuccess"],
            CancelUrl = configuration["Frontend:Payment:PaymentFailed"],
        };
        var service = new SessionService();
        Session session = await service.CreateAsync(options, cancellationToken: cancellationToken);

        return session.Id;
    }
}

