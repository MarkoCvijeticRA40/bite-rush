using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Products;
using SharedKernel;

namespace Application.Stripe.ProductPrices.Create;
internal sealed class CreateProductPriceCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateProductPriceCommand, string>
{
    public async Task<Result<string>> Handle(CreateProductPriceCommand request, CancellationToken cancellationToken)
    {
        var productPrice = new ProductPrice
        {
            Id = request.Id,
            Active = request.Active,
            BillingScheme = request.BillingScheme,
            Livemode = request.LiveMode,
            ProductId = request.ProductId,
            UnitAmount = request.UnitAmount,
            UnitAmountDecimal = request.UnitAmountDecimal,
        };

        await dbContext.ProductPrices.AddAsync(productPrice, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success<string>($"Product price with {productPrice.Id} successfully created.");
    }
}
