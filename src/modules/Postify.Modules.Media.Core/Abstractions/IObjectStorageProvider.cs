using System.Net.Mime;

namespace Postify.Modules.Media.Core.Abstractions;
public record UploadObjectStorageRequest(string BucketName, string ObjectId, Stream Data,string ContentType);


public interface IObjectStorageProvider
{

    public Task UploadObjectAsync(UploadObjectStorageRequest uploadRequest);

    public Task DeleteObjectAsync(string bucketName, string objectId);

    public Task<Stream> GetObjectAsync(string bucketName, string objectId);
}
