namespace Domain.Stripe.Webhooks;
public static class StripeEvents
{
    // Product events
    public const string ProductCreated = "product.created";
    public const string ProductUpdated = "product.updated";
    public const string ProductDeleted = "product.deleted";

    //Price events
    public const string ProductPriceCreated = "price.created";
    public const string ProductPriceUpdated = "price.updated";
    public const string ProductPriceDeleted = "price.deleted";
}
