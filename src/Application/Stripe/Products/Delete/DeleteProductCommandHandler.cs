using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Products.Delete;

internal sealed class DeleteProductCommandHandler(IApplicationDbContext context) : ICommandHandler<DeleteProductCommand, string>
{
    public async Task<Result<string>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        Product? product = await context.Products
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == command.ProductId, cancellationToken);

        if (product is null)
        {
            return Result.Failure<string>(ProductErrors.NotFound(command.ProductId));
        }

        try
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {

            return Result.Failure<string>(ProductErrors.DatabaseError(ex));
        }

        return Result.Success<string>($"Product with {product.Id} deleted successfully.");
    }
}
