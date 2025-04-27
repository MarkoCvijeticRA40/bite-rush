using Application.Abstractions.Messaging;

namespace Application.Products.Update;
public sealed record UpdateProductCommand(
    string Id,
    string Name,
    string Description,
    bool Active,
    List<string> Images,
    long Updated,
    bool LiveMode) : ICommand<string>;
