using Application.Stripe.Products.Update;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Products;

internal sealed class Update : IEndpoint
{
    public sealed record Request(string Id, string Title, string Description, List<string> Images, bool Active, long Updated, bool LiveMode);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products/update", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new UpdateProductCommand(
                request.Id,
                request.Title,
                request.Description,
                request.Active,
                request.Images,
                request.Updated,
                request.LiveMode);

            Result<string> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Products);
    }
}
