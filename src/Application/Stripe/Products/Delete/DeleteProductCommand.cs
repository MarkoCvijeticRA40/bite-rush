using Application.Abstractions.Messaging;

namespace Application.Products.Delete;
public sealed record DeleteProductCommand(string ProductId) : ICommand<string>;
