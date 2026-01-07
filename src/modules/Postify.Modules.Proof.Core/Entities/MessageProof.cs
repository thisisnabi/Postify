namespace Postify.Modules.Proof.Core.Entities;

public class MessageProof
{
    public long Id { get; set; }

    public string ProfileId { get; set; } = null!;
    public string Content { get; set; } = null!;
     
    public ICollection<string> FileNames { get; set; } = [];
    
    public DateTime CreatedAt { get; set; }

    public int SeenCount { get; set; }

    public MessageProofType ProofType { get; set; }
    public MessageProofStatus Status { get; set; }
}

public enum MessageProofType
{
    Text,
    Image,
    Video,
    Audio,
    Document
}

public enum MessageProofStatus
{
    Pending,
    Sent,
    Delivered,
    Opened,
}