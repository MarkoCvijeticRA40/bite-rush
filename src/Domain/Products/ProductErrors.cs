using SharedKernel;

namespace Domain.Products;
public static class ProductErrors
{
    public static Error NotFound(string productId) => Error.NotFound(
        "Products.NotFound",
        $"The product with the Id = '{productId}' was not found");

    public static Error Unauthorized() => Error.Failure(
        "Products.Unauthorized",
        "You are not authorized to perform this action.");
}
