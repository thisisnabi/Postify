using Amazon.S3;
using Amazon.S3.Model;
using Postify.Modules.Media.Core.Abstractions;

namespace Postify.Modules.Media.Infrastructure.Providers;

public class S3AmazonProvider : IObjectStorageProvider
{
    private readonly IAmazonS3 _s3Client;
    public S3AmazonProvider(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }

    public Task DeleteObjectAsync(string bucketName, string objectId)
    {
       return _s3Client.DeleteObjectAsync(bucketName, objectId);
    }

    public Task<Stream> GetObjectAsync(string bucketName, string objectId)
    {
       return _s3Client.GetObjectStreamAsync(bucketName, objectId, default);
    }

    public Task UploadObjectAsync(UploadObjectStorageRequest uploadRequest)
    {
        return _s3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = uploadRequest.BucketName,
            Key = uploadRequest.ObjectId,
            InputStream = uploadRequest.Data,
            ContentType = uploadRequest.ContentType
        });
    }
}
