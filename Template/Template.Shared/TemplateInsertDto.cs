using Newtonsoft.Json;

namespace Template.Shared
{
  
    
    public class TemplateUpdateDto
    {
        public string? Status { get; set; }
    }
    
    public class Language
    {
        [JsonProperty("langCode")]
        public string LangCode { get; set; }

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

    public class TemplateInsertDto
    {
        [JsonProperty("template")]
        public Template Template { get; set; }
    }

    public class Template
    {
        
        [JsonProperty("Id")]
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

        [JsonProperty("meta")]
        public List<Metum> Meta { get; set; }

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


}
