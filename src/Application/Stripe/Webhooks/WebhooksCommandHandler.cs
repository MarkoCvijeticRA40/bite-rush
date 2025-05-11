using Application.Abstractions.Messaging;
using Application.Stripe.ProductPrices.Create;
using Application.Stripe.ProductPrices.Update;
using Application.Stripe.Products.Create;
using Application.Stripe.Products.Delete;
using Application.Stripe.Products.Update;
using Domain.Stripe.Webhooks;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel;
using Stripe;
using StripeProduct = Stripe.Product;
using StripeProductPrice = Stripe.Price;

namespace Application.Stripe.Webhooks;
internal sealed class WebhooksCommandHandler(
    ILogger<WebhooksCommandHandler> logger,
    ISender sender) : ICommandHandler<WebhooksCommand, string>
{
    public async Task<Result<string>> Handle(WebhooksCommand command, CancellationToken cancellationToken)
    {
        Event stripeEvent = command.stripeEvent;

        switch (stripeEvent.Type)
        {
            case StripeEvents.ProductCreated:
                if (stripeEvent.Data.Object is StripeProduct createdProduct)
                {
                    await sender.Send(new CreateProductCommand(Id: createdProduct.Id, LiveMode: createdProduct.Livemode, Name: createdProduct.Name, Description: createdProduct.Description, Active: createdProduct.Active, Images: createdProduct.Images, Updated: new DateTimeOffset(createdProduct.Updated).ToUnixTimeSeconds()), cancellationToken);
                }
                break;

            case StripeEvents.ProductUpdated:
                if (stripeEvent.Data.Object is StripeProduct updatedProduct)
                {
                    await sender.Send(new UpdateProductCommand(Id: updatedProduct.Id, Updated: new DateTimeOffset(updatedProduct.Updated).ToUnixTimeSeconds(), LiveMode: updatedProduct.Livemode, Name: updatedProduct.Name, Description: updatedProduct.Description, Active: updatedProduct.Active, Images: updatedProduct.Images), cancellationToken);
                }
                break;

            case StripeEvents.ProductDeleted:
                if (stripeEvent.Data.Object is StripeProduct deletedProduct)
                {
                    await sender.Send(new DeleteProductCommand(ProductId: deletedProduct.Id), cancellationToken);
                }
                break;

            case StripeEvents.ProductPriceCreated:
                if (stripeEvent.Data.Object is StripeProductPrice createdProductPrice)
                {
                    await sender.Send(new CreateProductPriceCommand(Id: createdProductPrice.Id, Active: createdProductPrice.Active,
                        BillingScheme: createdProductPrice.BillingScheme, Currency: createdProductPrice.Currency, LiveMode: createdProductPrice.Livemode,
                        ProductId: createdProductPrice.ProductId, UnitAmount: createdProductPrice.UnitAmount, UnitAmountDecimal: createdProductPrice.UnitAmountDecimal), cancellationToken);
                }
                break;

            case StripeEvents.ProductPriceUpdated:
                if (stripeEvent.Data.Object is StripeProductPrice updatedProductPrice)
                {
                    await sender.Send(new UpdateProductPriceCommand(Id: updatedProductPrice.Id, Active: updatedProductPrice.Active,
                        BillingScheme: updatedProductPrice.BillingScheme, Currency: updatedProductPrice.Currency, LiveMode: updatedProductPrice.Livemode,
                        ProductId: updatedProductPrice.ProductId, UnitAmount: updatedProductPrice.UnitAmount, UnitAmountDecimal: updatedProductPrice.UnitAmountDecimal), cancellationToken);
                }
                break;

            case StripeEvents.ProductPriceDeleted:
                if (stripeEvent.Data.Object is StripeProductPrice deletedProductPrice)
                {
                    await sender.Send(new DeleteProductCommand(ProductId: deletedProductPrice.Id), cancellationToken);
                }
                break;

            default:
                logger.LogInformation("Unhandled Stripe event type: {EventType}", stripeEvent.Type);
                break;
        }

        logger.LogInformation("Stripe Webhook received: {Event}", stripeEvent.Type);
        return Result.Success("Webhook processed successfully");
    }
}
