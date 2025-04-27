using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Products;
using SharedKernel;

namespace Application.Products.Create;
internal sealed class CreateProductCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateProductCommand, string>
{
    public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            Images = request.Images,
            Active = request.Active,
            LiveMode = request.LiveMode,
            Updated = request.Updated,
        };

        await dbContext.Products.AddAsync(product, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success<string>($"Product with {product.Id} successfully created.");
    }
}
