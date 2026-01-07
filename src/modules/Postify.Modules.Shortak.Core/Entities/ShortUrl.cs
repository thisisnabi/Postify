namespace Postify.Modules.Shortak.Core.Entities;

public class ShortUrl
{
    public string Id { get; set; } = null!;

    public string OriginalUrl { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public int ClickCount { get; set; }
}
