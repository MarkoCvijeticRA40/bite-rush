using Application.Abstractions.Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace Infrastructure.Azure;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _basePath;

    public BlobStorageService(string connectionString, string basePath)
    {
        _blobServiceClient = new BlobServiceClient(connectionString);
        _basePath = basePath;
    }

    public List<string> GetSecureBlobUrls(List<string> blobs)
    {
        return blobs?.Select(blobUrl =>
        {
            if (!string.IsNullOrWhiteSpace(_basePath) && !string.IsNullOrWhiteSpace(blobUrl) && blobUrl.StartsWith(_basePath, StringComparison.OrdinalIgnoreCase))
            {
                var uri = new Uri(blobUrl);
                string relativePath = uri.AbsolutePath.TrimStart('/');

                // Extract container name and blob path
                string[] pathParts = relativePath.Split('/', 2);
                if (pathParts.Length == 2)
                {
                    string containerName = pathParts[0];
                    string blobPath = pathParts[1];

                    return GetSecureBlobUrl(blobPath, containerName).ToString();
                }
            }

            return blobUrl; // fallback to original if not valid Azure blob URL
        }).ToList() ?? new List<string>();
    }

    private Uri GetSecureBlobUrl(string blobPath, string _containerName, int expiryMinutes = 15)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        BlobClient blobClient = containerClient.GetBlobClient(blobPath);

        if (!blobClient.CanGenerateSasUri)
        {
            throw new InvalidOperationException("Cannot generate SAS URI. Ensure the client uses shared key credentials.");
        }

        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = _containerName,
            BlobName = blobPath,
            Resource = "b", // blob
            ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(expiryMinutes)
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        Uri sasUri = blobClient.GenerateSasUri(sasBuilder);
        return sasUri;
    }
}
