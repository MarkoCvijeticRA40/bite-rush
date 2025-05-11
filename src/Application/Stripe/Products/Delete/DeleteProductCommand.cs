using Application.Abstractions.Messaging;

namespace Application.Stripe.Products.Delete;

public sealed record DeleteProductCommand(string ProductId) : ICommand<string>;
