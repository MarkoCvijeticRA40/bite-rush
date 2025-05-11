using Application.Abstractions.Messaging;

namespace Application.Stripe.Products.Get;

public sealed record GetProductsQuery() : IQuery<List<ProductResponse>>;
