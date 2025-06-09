using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Template.Repository.Model;

    public class Language
    {
        public string LanguageCode { get; set; }
        public bool IsDeleted { get; set; }
        public string Heading { get; set; }
        public string Subheading { get; set; }
        public string Body { get; set; }
        public ResponseOption ResponseOptions { get; set; }
        public List<RichMedium> RichMedia { get; set; }
    }

    public class Metum
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class NotificationMedium
    {
        public string ChannelId { get; set; }
        public string TenantId { get; set; }
    }

    public class ResponseOption
    {
        public string Name { get; set; }
        public string ReponseOptionTypeld { get; set; }
        public string IsPrimary { get; set; }
        public string IsExposeOnPush { get; set; }
        public string Action { get; set; }
        public string Value { get; set; }
        public List<Metum> Meta { get; set; }
        public string Label { get; set; }
    }

    public class RichMedium
    {
        public string Name { get; set; }
        public string ContentTypeld { get; set; }
        public string DisplayTypeld { get; set; }
        public string CmsSourceTypeld { get; set; }
        public string Token { get; set; }
        public string DisplayName { get; set; }
        public string SourcePath { get; set; }
    }

    public class Templates
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string TemplateCode { get; set; }
        public int TemplateVersion { get; set; }
        public string Description { get; set; }
        public string CurrentStatus { get; set; }
        public string BackOfficeld { get; set; }
        public string Datecreated { get; set; }
        public string DateLastUpdated { get; set; }
        public string IsPush { get; set; }
        public string IslnApp { get; set; }
        public string IsDirectDeeplink { get; set; }
        public string IsShowExpiry { get; set; }
        public string NotficationTypeld { get; set; }
        public string NotficationCategoryTypeld { get; set; }
        public List<Metum> Meta { get; set; }
        public List<Tokens> Tokens { get; set; }
        public List<RichMedium> RichMedia { get; set; }
        public List<ResponseOption> ResponseOptions { get; set; }
        public List<Language> Languages { get; set; }
        public List<NotificationMedium> NotificationMedium { get; set; }
        public List<Statuses> Status { get; set; }
    }

    public class Statuses
    {
        public string User { get; set; }
        public string Status { get; set; }
        public string ActionDateTime { get; set; }
    }
    public class Tokens
    {
        public string Name { get; set; }
        public string Token { get; set; }
        public string TokenTypeld { get; set; }
    }



