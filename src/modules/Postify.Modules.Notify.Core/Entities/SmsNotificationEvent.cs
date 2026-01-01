namespace Postify.Modules.Notify.Core.Entities;

public class SmsNotificationEvent
{
    public long Id { get; set; }

    public string TrackingCode { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public SmsNotificationEventType EventType { get; set; }

    public string Text { get; set; } = null!;

    public string? RemoteId { get; set; }

    public DateTime CreatedAt { get; set; }
}

public enum SmsNotificationEventType
{
    NotificationCreated,
    NotificationSent,
    NotificationDelivered,
    NotificationFailed,
    LinkClicked
}