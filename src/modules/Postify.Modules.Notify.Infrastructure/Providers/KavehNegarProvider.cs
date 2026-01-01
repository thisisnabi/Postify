using Kavenegar;
using Kavenegar.Core.Models.Enums;
using Microsoft.Extensions.Options;
using Postify.Modules.Notify.Core.Abstractions;
using Postify.Modules.Notify.Core.Entities;
using Postify.Shared.Kernel;
using Postify.Shared.Kernel.Errors;
using System.Runtime;

namespace Postify.Modules.Notify.Infrastructure.Providers;

public class KavehNegarProvider : ISmsProvider
{
    private readonly KavehNegarSettings _settings;

    public KavehNegarProvider(IOptions<AppSettings> options)
    {
        _settings = options.Value.Modules.Sms.KavehNegar;
    }

    private readonly static List<int> SucceedSendStatus = [1, 2, 4, 5, 10];

    public async Task<SendSmsProviderResult> SendAsync(string phoneNumber, string message)
    {
        var api = new KavenegarApi(_settings.ApiKey);
        var result = await api.Send(_settings.SenderNumber, phoneNumber, message);

        if (SucceedSendStatus.Exists(x => x == result.Status))
            return new SendSmsProviderResult(result.Messageid.ToString());

        throw new ServiceErrorException(Error.Unexpected());
    }

    private readonly static List<int> SucceedDeliveryStatus = [4, 5, 10, 50];
    public async Task<SmsNotificationEventType> InquiryAsync(string remoteId)
    {
        try
        {
            var api = new KavenegarApi(_settings.ApiKey);
            var results = await api.Status([remoteId]);

            var transactionItem = results[0];
            if (SucceedDeliveryStatus.Exists(x => x == (int)transactionItem.Status))
                return SmsNotificationEventType.NotificationDelivered;

            if (transactionItem.Status == MessageStatus.Queued)
                return SmsNotificationEventType.NotificationSent;

            return SmsNotificationEventType.NotificationFailed;
        }
        catch
        {
            return SmsNotificationEventType.NotificationFailed;
        }
    }

}
