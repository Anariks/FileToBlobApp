using Azure.Storage.Blobs;
using Exceptions;
using Models;

namespace Services;

public class FileService
{
    private readonly BlobContainerClient _blobContainer;

    public FileService(BlobContainerClient blobContainer)
    {
        _blobContainer = blobContainer;
    }

    public async Task UploadToStorage(FormData formData)
    {
        string systemFileName = formData.file.FileName;

        //var checkBlob = _blobContainer.blob
        var blob = _blobContainer.GetBlobClient(systemFileName);

        if (await blob.ExistsAsync())
            throw new AppException(
                ErrorCode.BadRequest,
                $"File with name {systemFileName} already exists, change the name"
            );

        await using (var data = formData.file.OpenReadStream())
        {
            await blob.UploadAsync(data);
            await blob.SetMetadataAsync(
                new Dictionary<string, string> { { "email", formData.email } }
            );
        }

        return;
    }
}
