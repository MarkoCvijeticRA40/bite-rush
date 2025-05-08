using Application.Abstractions.Messaging;

namespace Application.Products.Create;
public sealed record class CreateProductCommand(
    string Id,
    string Name, 
    string Description, 
    bool Active, 
    List<string> Images,
    long Updated,
    bool LiveMode) : ICommand<string>;
