namespace Application.Abstractions.Azure;

public interface IBlobStorageService
{
    List<string> GetSecureBlobUrls(List<string> blobs);
}
