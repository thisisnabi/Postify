namespace Postify.Modules.Profile.Core.Payloads;

public class ProfileResponse
{
    public long Id { get; set; }
    public string NationalId { get; set; } = string.Empty;
    public string ProfileType { get; set; } = string.Empty;
}

