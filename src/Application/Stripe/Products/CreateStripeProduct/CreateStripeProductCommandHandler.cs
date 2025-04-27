using Application.Abstractions.Messaging;
using SharedKernel;
using Stripe;

namespace Application.Products.Create;
internal sealed class CreateStripeProductCommandHandler() : ICommandHandler<CreateStripeProductCommand, string>
{
    public async Task<Result<string>> Handle(CreateStripeProductCommand request, CancellationToken cancellationToken)
    {
        //Create Product
        var product = new ProductCreateOptions 
        { 
            Id = Guid.NewGuid().ToString(), 
            Name = request.Name,
            Description = request.Description,
            Images = request.Images,
            Active = request.Active,
        };
        var productService = new ProductService();
        await productService.CreateAsync(product, cancellationToken: cancellationToken);

        //Create Product Price
        var priceOptions = new PriceCreateOptions
        {
            UnitAmount = request.Price,
            Currency = request.Currency,
            Product = product.Id,
        };
        var priceService = new PriceService();
        await priceService.CreateAsync(priceOptions, cancellationToken: cancellationToken);

        return Result.Success<string>($"Product with {product.Id} successfully created.");
    }
}
