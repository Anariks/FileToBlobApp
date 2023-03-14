using Azure.Storage.Blobs;
using Services;

public static class BlobContainerExtensions
{
    public static IServiceCollection AddBlobContainer(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton(x =>
        {
            string blobConnectionString = configuration.GetValue<string>("BlobConnectionString");
            string blobContainerName = configuration.GetValue<string>("BlobContainerName");

            return new BlobContainerClient(blobConnectionString, blobContainerName);
        });
        return services;
    }
}
