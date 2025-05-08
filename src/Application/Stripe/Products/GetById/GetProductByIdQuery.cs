using Application.Abstractions.Messaging;

namespace Application.Products.GetById;
public sealed record GetProductByIdQuery(string Id) : IQuery<ProductResponse>;
