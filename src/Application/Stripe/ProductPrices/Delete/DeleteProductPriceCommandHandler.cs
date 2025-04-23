using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Stripe.ProductPrices.Delete;
internal sealed class DeleteProductPriceCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteProductPriceCommand, string>
{
    public async Task<Result<string>> Handle(DeleteProductPriceCommand command, CancellationToken cancellationToken)
    {
        ProductPrice productPrice = await dbContext.ProductPrices
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == command.ProductPriceId, cancellationToken);
        if (productPrice is null)
        {
            return Result.Failure<string>(ProductPriceErrors.NotFound(command.ProductPriceId));
        }
        dbContext.ProductPrices.Remove(productPrice);
        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success<string>($"Product price with {productPrice.Id} deleted successfully.");
    }
}
