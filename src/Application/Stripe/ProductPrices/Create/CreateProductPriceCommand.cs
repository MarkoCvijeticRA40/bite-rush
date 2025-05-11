using Application.Abstractions.Messaging;

namespace Application.Stripe.ProductPrices.Create;

public sealed record class CreateProductPriceCommand(string Id, bool Active, string BillingScheme, string Currency, 
    bool LiveMode, string ProductId, long? UnitAmount, decimal? UnitAmountDecimal) : ICommand<string>;
