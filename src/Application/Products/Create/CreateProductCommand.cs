using Application.Abstractions.Messaging;

namespace Application.Products.Create;
public sealed record CreateProductCommand(string Name, string Description, string Currency, long Price, bool Active, List<string> Images) : ICommand<string>;
