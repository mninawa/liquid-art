using AutoMapper;
using Template.Repository.Model;
using Template.Shared;
using System.Diagnostics.CodeAnalysis;
using Language = Template.Shared.Language;
using Metum = Template.Shared.Metum;
using ResponseOption = Template.Shared.ResponseOption;
using RichMedium = Template.Shared.RichMedium;

namespace Template.Business.Profiles
{
    public sealed class AssemblyMarker
    {
        AssemblyMarker() { }
    }

    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
    public class InputFileProfile : Profile
    {
        public InputFileProfile()
        {
            CreateMap<Shared.Template, Templates>().ReverseMap();
            CreateMap<Metum, Repository.Model.Metum>().ReverseMap();
            CreateMap<RichMedium, Repository.Model.RichMedium>().ReverseMap();
            CreateMap<Language,Repository.Model.Language>().ReverseMap();
            CreateMap<Shared.Token, Repository.Model.Token>().ReverseMap();
            CreateMap<ResponseOption, Repository.Model.ResponseOption>().ReverseMap();
            CreateMap<Templates, Shared.Template>().ReverseMap();
            
            /***
             *  CreateMap<Shared.Template, Templates>()
                          .ForMember(s => s.Tokens, o => o.Condition(src => src.Tokens != null))
                          .ForMember(s => s.Metum, o => o.Condition(src => src.Metum != null))
                          .ForMember(s => s.RichMedia, o => o.Condition(src => src.RichMedia != null))
                          .ForMember(s => s.Languages, o => o.Condition(src => src.Languages != null))
                         // .ForMember(s => s.NotificationMedium, o => o.Condition(src => src.NobficationType != null))
                          .ForMember(s => s.ResponseOptions, o => o.Condition(src => src.ResponseOptions != null)).ReverseMap();
                      
                          CreateMap<Templates, Shared.Template>()     
                              .ForMember(s => s.Tokens, o => o.Condition(src => src.Tokens != null))
                              .ForMember(s => s.Metum, o => o.Condition(src => src.Metum != null))
                              .ForMember(s => s.RichMedia, o => o.Condition(src => src.RichMedia != null))
                              .ForMember(s => s.Languages, o => o.Condition(src => src.Languages != null))
                              // .ForMember(s => s.NotificationMedium, o => o.Condition(src => src.NobficationType != null))
                              .ForMember(s => s.ResponseOptions, o => o.Condition(src => src.ResponseOptions != null)).ReverseMap();
             */
          
        }
    }
}
