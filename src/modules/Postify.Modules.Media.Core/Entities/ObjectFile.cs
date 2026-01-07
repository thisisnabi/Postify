namespace Postify.Modules.Media.Core.Entities;

public class ObjectFile
{
    public Guid Id { get; set; }

    public string FileName { get; set; } = null!;

    public string ContentType { get; set; } = null!;

    public long SizeInBytes { get; set; }

    public DateTime CreatedAt { get; set; }

    public string StorageObjectId { get; set; } = null!;

    public string BucketName { get; set; } = null!;

    public ObjectFileAccessLevel AccessLevel { get; set; }

    public ObjectFileStatus Status { get; set; }

    public string UploadedByUserId { get; set; } = null!;
}


public enum ObjectFileStatus
{
    Temporary,
    Permanent,
    Deleted,
    Archived
}

public enum ObjectFileAccessLevel
{
    Private,
    PublicRead,
    PublicReadWrite,
    Protected
}