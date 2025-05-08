using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Stripe.ProductPrices.Update;

internal sealed class UpdateProductPriceCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateProductPriceCommand, string>
{
    public async Task<Result<string>> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
    {
        Domain.Products.ProductPrice existingProductPrice = await dbContext.ProductPrices.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (existingProductPrice != null && request != null)
        {
            existingProductPrice.Active = request.Active;
            existingProductPrice.BillingScheme = "Updated";
            existingProductPrice.Currency = request.Currency;
            existingProductPrice.Livemode = request.LiveMode;
            existingProductPrice.ProductId = request.ProductId;
            existingProductPrice.UnitAmount = request.UnitAmount;
            existingProductPrice.UnitAmountDecimal = request.UnitAmountDecimal;
        }

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {

            return Result.Failure<string>(ProductPriceErrors.DatabaseError(ex));
        }

        return Result.Success<string>($"Product Price with {request?.Id} successfully updated.");
    }
}
