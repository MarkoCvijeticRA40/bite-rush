using Application.Abstractions.Messaging;

namespace Application.Stripe.ProductPrices.Update;
public sealed record class UpdateProductPriceCommand(
    string Id, 
    bool Active,
    string BillingScheme,
    string Currency,
    bool LiveMode,
    string ProductId,
    long? UnitAmount,
    decimal? UnitAmountDecimal) : ICommand<string>;
