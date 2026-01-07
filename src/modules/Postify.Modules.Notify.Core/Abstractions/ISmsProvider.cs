using Postify.Modules.Notify.Core.Entities;

namespace Postify.Modules.Notify.Core.Abstractions;

public record SendSmsProviderResult(string RemoteId);
 
public interface ISmsProvider
{
    Task<SendSmsProviderResult> SendAsync(string phoneNumber, string message);

    Task<SmsNotificationEventType> InquiryAsync(string remoteId);
}
