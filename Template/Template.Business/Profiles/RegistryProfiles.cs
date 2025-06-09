using AutoMapper;
using Template.Repository.Model;
using Template.Shared;
using System.Diagnostics.CodeAnalysis;

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
            CreateMap<TemplateInsertDto, Templates>().ReverseMap();
            CreateMap<Templates, TemplateInsertDto>().ReverseMap();
          
        }
    }
}
