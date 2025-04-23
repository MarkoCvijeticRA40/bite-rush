using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Products;
using SharedKernel;

namespace Application.Stripe.ProductPrices.Update;
internal sealed class UpdateProductPriceCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateProductPriceCommand, string>
{
    public async Task<Result<string>> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
    {
        var product = new ProductPrice
        {
            Active = request.Active,
            BillingScheme = request.BillingScheme,
            Currency = request.Currency,
            Livemode = request.LiveMode,
            ProductId = request.ProductId,
            UnitAmount = request.UnitAmount,
            UnitAmountDecimal = request.UnitAmountDecimal,
        };

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success<string>($"Product Price with {request.Id} successfully updated.");
    }
}
