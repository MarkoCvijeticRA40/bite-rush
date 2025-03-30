using Application.Products.Create;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Products;

internal sealed class Create : IEndpoint
{
    public sealed record Request(string Title, string Description, List<string> Images, long Price, string Currency, bool Active);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products/create", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateProductCommand(
                request.Title,
                request.Description,
                request.Currency,
                request.Price,
                request.Active,
                request.Images);

            Result<string> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Products);
    }
}
