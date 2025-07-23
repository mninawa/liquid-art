using AutoMapper;
using Registry.Repository.Model;
using Registry.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Registry.Business.Profiles
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
            CreateMap<ClientInsertDto, Client>().ReverseMap();
            CreateMap<Client, ClientReadDto>().ReverseMap();
            CreateMap<DeviceInsertDto, Device>().ReverseMap();
            CreateMap<Device, DeviceReadDto>().ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<Coupon, CouponDto>().ReverseMap();
        }
    }
}
