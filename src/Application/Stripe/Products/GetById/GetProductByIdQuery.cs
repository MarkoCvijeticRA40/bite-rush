using Application.Abstractions.Messaging;

namespace Application.Stripe.Products.GetById;
public sealed record GetProductByIdQuery(string Id) : IQuery<ProductResponse>;
