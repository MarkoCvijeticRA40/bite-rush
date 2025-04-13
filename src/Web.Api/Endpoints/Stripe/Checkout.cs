using Application.Stripe.CheckoutOrder;
using Domain.Stripe;
using MediatR;
using SharedKernel;

namespace Web.Api.Endpoints.Stripe;

internal sealed class Checkout : IEndpoint
{
    public sealed class Request
    {
        public List<OrderItem> Items { get; init; }

        public string Currency { get; init; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("stripe/checkout", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CheckoutCommand
            {
                Items = request.Items,
                Currency = request.Currency,
            };

            Result<string> result = await sender.Send(command, cancellationToken);

            return Results.Ok(new { result.Value });
        })
        .WithTags(Tags.Stripe);
        //.RequireAuthorization();
    }
}
