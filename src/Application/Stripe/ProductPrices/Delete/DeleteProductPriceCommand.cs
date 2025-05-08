using Application.Abstractions.Messaging;

namespace Application.Stripe.ProductPrices.Delete;

public sealed record class DeleteProductPriceCommand(string ProductPriceId) : ICommand<string>;

