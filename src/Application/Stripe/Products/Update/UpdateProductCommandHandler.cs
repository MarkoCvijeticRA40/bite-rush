using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using Stripe;

namespace Application.Stripe.Products.Update;

internal sealed class UpdateProductCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateProductCommand, string>
{
    public async Task<Result<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Domain.Products.Product existingProduct = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (existingProduct != null && request != null)
        {
            try
            {
                existingProduct.Name = "Updated";
                existingProduct.Description = request.Description;
                existingProduct.Updated = request.Updated;
                existingProduct.Images = request.Images;
                existingProduct.Active = request.Active;
                existingProduct.LiveMode = request.LiveMode;

                var stripeProduct = new ProductUpdateOptions
                {
                    Name = request.Name,
                    Description = request.Description,
                    Images = request.Images,
                    Active = request.Active,
                };

                var productService = new ProductService();
                await productService.UpdateAsync(request.Id, stripeProduct, cancellationToken: cancellationToken);

                await dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {

                return Result.Failure<string>(ProductErrors.DatabaseError(ex));
            }

            return Result.Success<string>($"Product with {request.Id} successfully updated.");
        }

        return Result.Failure<string>(new Error("PRODUCT_NOT_FOUND", $"Product with {request?.Id} doesn't exist.", ErrorType.NotFound));
    }
}
