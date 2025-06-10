using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Template.Repository.Model;


    public class Language
    {
        [JsonProperty("langCode")]
        public string LanguageCode { get; set; }

        [JsonProperty("heading")]
        public string Heading { get; set; }

        [JsonProperty("subheading")]
        public string Subheading { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("responseOptions")]
        public List<ResponseOption> ResponseOptions { get; set; }

        [JsonProperty("richMedia")]
        public List<RichMedium> RichMedia { get; set; }
    }

    public class Metum
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class ResponseOption
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("isPrimary")]
        public string IsPrimary { get; set; }

        [JsonProperty("isExposeOnPush")]
        public string IsExposeOnPush { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("meta")]
        public List<Metum> Meta { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }

    public class RichMedium
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("displayTypes")]
        public string DisplayTypes { get; set; }

        [JsonProperty("sourceCms")]
        public string SourceCms { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("sourcePath")]
        public string SourcePath { get; set; }
    }

    public class Templates
    {
        
        [JsonProperty("Id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [JsonProperty("templateCode")]
        public string TemplateCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("isPush")]
        public string IsPush { get; set; }

        [JsonProperty("isinApp")]
        public string IsinApp { get; set; }

        [JsonProperty("isDirectDeeplink")]
        public string IsDirectDeeplink { get; set; }

        [JsonProperty("isShowExpiry")]
        public string IsShowExpiry { get; set; }

        [JsonProperty("nobficationType")]
        public string NobficationType { get; set; }

        [JsonProperty("metum")]
         public List<Metum> Metum { get; set; }

        [JsonProperty("tokens")]
        public List<Token> Tokens { get; set; }

        [JsonProperty("richMedia")]
        public List<RichMedium> RichMedia { get; set; }

        [JsonProperty("responseOptions")]
        public List<ResponseOption> ResponseOptions { get; set; }

        [JsonProperty("languages")]
        public List<Language> Languages { get; set; }
    }

    public class Token
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("token")]
        public string Tokens { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
    // public class Language
    // {
    //     public string LanguageCode { get; set; }
    //   //  public string IsDeleted { get; set; }
    //     public string Heading { get; set; }
    //     public string Subheading { get; set; }
    //     public string Body { get; set; }
    //
    //     public List<ResponseOption> ResponseOptions { get; set; }
    //     public List<RichMedium> RichMedia { get; set; }
    // }
    //
    // public class Metum
    // {
    //     public string Name { get; set; }
    //     public string Value { get; set; }
    // }
    //
    // public class NotificationMedium
    // {
    //     public string ChannelId { get; set; }
    //     public string TenantId { get; set; }
    // }
    //
    // public class ResponseOption
    // {
    //     public string Name { get; set; }
    //     public string ReponseOptionTypeld { get; set; }
    //     public string IsPrimary { get; set; }
    //     public string IsExposeOnPush { get; set; }
    //     public string Action { get; set; }
    //     public string Value { get; set; }
    //     public List<Metum> Meta { get; set; }
    //     public string Label { get; set; }
    // }
    //
    // public class RichMedium
    // {
    //     public string Name { get; set; }
    //     public string ContentTypeld { get; set; }
    //     public string DisplayTypeld { get; set; }
    //     public string CmsSourceTypeld { get; set; }
    //     public string Token { get; set; }
    //     public string DisplayName { get; set; }
    //     public string SourcePath { get; set; }
    // }
    //
    // public class Templates
    // {
    //     [BsonId]
    //     [BsonRepresentation(BsonType.ObjectId)]
    //     public string Id { get; set; }
    //     public string TemplateCode { get; set; }
    //     public int TemplateVersion { get; set; }
    //     public string Description { get; set; }
    //     public string CurrentStatus { get; set; }
    //     public string BackOfficeld { get; set; }
    //     public string Datecreated { get; set; }
    //     public string DateLastUpdated { get; set; }
    //     public string IsPush { get; set; }
    //     public string IslnApp { get; set; }
    //     public string IsDirectDeeplink { get; set; }
    //     public string IsShowExpiry { get; set; }
    //     public string NotficationTypeld { get; set; }
    //     public string NotficationCategoryTypeld { get; set; }
    //     public List<Metum> Metum { get; set; } = new();
    //     public List<Tokens> Tokens { get; set; }= new();
    //     public List<RichMedium> RichMedia { get; set; }= new();
    //     public List<ResponseOption> ResponseOptions { get; set; }= new();
    //     public List<Language> Languages { get; set; }= new();
    //     //public List<NotificationMedium> NotificationMedium { get; set; }= new();
    //     public List<Statuses> Status { get; set; }= new();
    // }
    //
    // public class Statuses
    // {
    //     public string User { get; set; }
    //     public string Status { get; set; }
    //     public string ActionDateTime { get; set; }
    // }
    // public class Tokens
    // {
    //     public string Name { get; set; }
    //     public string Token { get; set; }
    //     public string TokenTypeld { get; set; }
    // }



