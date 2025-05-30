namespace Notifications.Shared
{
    public class NotificationsDetailInsertDto
    {
        public string ClientId { get; set; }
        public string Account { get; set; }
        public string Amount { get; set; }
        public string Balance { get; set; }
        public string Merchant { get; set; }
        public string Date { get; set; }
        public string TemplateId { get; set; }
    }
    
    public class NotificationReadDto
    {
        public string Id { get; set; }
        public string? Mobile { get; set; }
        public string Content { get; set; }
        public string Status { get; set; } = "Unread";
    }
    public static class Parser
    {
        public static string Parse(this string template, Dictionary<string, string> replacements)
        {
            if (replacements.Count > 0)
            {
                template = replacements.Keys
                    .Aggregate(template, (current, key) => current.Replace(key, replacements[key]));
            }
            return template;
        }
    }
}

