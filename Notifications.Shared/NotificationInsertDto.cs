namespace Notifications.Shared
{
    public class NotificationInsertDto
    {
        public string Id { get; set; }
        public List<NotificationsDetailInsertDto> NotificationsDetail { get; set; } = new();
    }

    public class NotificationsReadDto
    {
        public string Id { get; set; }
        public string messageId { get; set; }
        public string ClientId { get; set; }
        public string Amount { get; set; }
        public string Balance { get; set; }
        public string? Mobile { get; set; }
        public string Content { get; set; }
        public string Status { get; set; } = "Unread";
        public DateTime EventDate { get; set; }
    }
}


