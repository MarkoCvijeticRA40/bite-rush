using Domain.Stripe.Checkout;

namespace Application.Abstractions.Services;
public interface ICheckoutQueueService
{
    Task SendCheckoutMessageAsync(CheckoutCompletedNotification notification, CancellationToken cancellationToken = default);
}
