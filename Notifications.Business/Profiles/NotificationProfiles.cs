using System;
using System.Collections.Generic;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using Notifications.Repository.Model;
using Notifications.Shared;

namespace Notifications.Business.Profiles
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
            CreateMap<NotificationInsertDto, Notification>().ReverseMap();
            CreateMap<NotificationsDetailInsertDto, Notification>().ReverseMap();
            CreateMap<Notification, NotificationsReadDto>().ReverseMap();
        }
    }
}
