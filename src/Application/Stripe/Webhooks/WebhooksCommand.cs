using Application.Abstractions.Messaging;
using Stripe;

namespace Application.Stripe.Webhooks;
public sealed record class WebhooksCommand(Event stripeEvent) : ICommand<string>;

