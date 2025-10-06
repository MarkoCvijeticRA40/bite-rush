/*using System.Text.Json;
using Application.Abstractions.Services;
using Azure.Storage.Queues;
using Domain.Stripe.Checkout;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class CheckoutQueueService : ICheckoutQueueService
{
    private readonly QueueClient _queueClient;
    private readonly ILogger<CheckoutQueueService> _logger;
    private readonly IConfiguration _configuration;
    
    public CheckoutQueueService(IConfiguration configuration, ILogger<CheckoutQueueService> logger, string connectionString)
    {
        _logger = logger;
        _configuration = configuration;
        
        _configuration = configuration["AzureStorage:ConnectionString"];
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Azure Storage connection string is not configured.");
        }

        // Initialize QueueClient
        _queueClient = new QueueClient(connectionString, "stripe-checkout-queue");
    }

    public async Task SendCheckoutMessageAsync(CheckoutCompletedNotification notification, CancellationToken cancellationToken = default)
    {
        try
        {
            // Ensure queue exists
            await _queueClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

            // Map to queue message format
            var queueMessage = new StripeCheckoutQueueMessage
            {
                SessionId = notification.SessionId,
                Currency = notification.Currency,
                CustomerEmail = notification.CustomerEmail,
                CustomerName = notification.CustomerName,
                CheckoutDate = notification.CheckoutDate,
                TotalAmount = notification.TotalAmount,
                PaymentStatus = notification.PaymentStatus,
                PaymentIntentId = notification.PaymentIntentId,
                Items = notification.Items.Select(MapToQueueItem).ToList()
            };

            // Serialize and send to queue
            var messageJson = JsonSerializer.Serialize(queueMessage, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await _queueClient.SendMessageAsync(messageJson, cancellationToken);

            _logger.LogInformation("Stripe checkout message sent to queue for session {SessionId}", notification.SessionId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send stripe checkout message for session {SessionId}", notification.SessionId);
            throw;
        }
    }

    private static StripeCheckoutQueueItem MapToQueueItem(OrderItem item)
    {
        return new StripeCheckoutQueueItem
        {
            ItemId = item.ItemId,
            PriceId = item.PriceId,
            Quantity = item.Quantity
        };
    }
}*/
