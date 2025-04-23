using SharedKernel;

namespace Domain.Products;
public static class ProductPriceErrors
{
    public static Error NotFound(string productPriceId) => Error.NotFound(
        "ProductPrices.NotFound",
        $"The product price with the Id = '{productPriceId}' was not found");

    public static Error Unauthorized() => Error.Failure(
        "ProductPrices.Unauthorized",
        "You are not authorized to perform this action.");
}
