using System.Text;
using System.Text.Json;
using Application.Abstractions.Messaging;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace Application.Azure;

public sealed class AzureQueueEventBusHandler(QueueServiceClient queueServiceClient) : IEventBus
{
    private readonly QueueClient _queueClient = queueServiceClient.GetQueueClient("user-events");

    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken) where T : class
    {
        await _queueClient.CreateIfNotExistsAsync(metadata: null, cancellationToken: cancellationToken);

        string messagePayload = JsonSerializer.Serialize(message);

        byte[] bytes = Encoding.UTF8.GetBytes(messagePayload);

        SendReceipt response = await _queueClient.SendMessageAsync(Convert.ToBase64String(bytes), cancellationToken);
    }
}
