using Application.Stripe.Products.Delete;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Products;

public class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("products/{productId}", async (string productId, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new DeleteProductCommand(productId);

            Result<string> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        //.HasPermission(Permissions.UsersAccess)
        .WithTags(Tags.Products);
    }
}
