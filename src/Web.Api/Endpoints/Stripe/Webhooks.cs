using MediatR;
using SharedKernel;
using Stripe;
using Application.Stripe.Webhooks;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Endpoints.Stripe;
public class Webhooks : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("stripe/webhooks", async (HttpRequest request, ISender sender, [FromHeader(Name = "Stripe-Signature")] string stripeSignature, CancellationToken cancellationToken) =>
        {
            using var reader = new StreamReader(request.Body);
            string json = await reader.ReadToEndAsync(cancellationToken);

            Event stripeEvent = EventUtility.ConstructEvent(
                json,
                stripeSignature,
                "whsec_43dbeb2c460dc57b4e1f5871567fae54714b28f2350eb486f9617c65419e09fe"
            );

            var command = new WebhooksCommand(stripeEvent);
            Result<string> result = await sender.Send(command, cancellationToken);

            return Results.Ok(result);
        })
        .WithTags(Tags.Stripe);
    }
}
