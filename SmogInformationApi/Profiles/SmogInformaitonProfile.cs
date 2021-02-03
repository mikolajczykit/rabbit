using AutoMapper;
using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmogInformationApi.Profiles
{
    public class SmogInformaitonProfile : Profile
    {
        public SmogInformaitonProfile()
        {
            CreateMap<SmogInformation, SmogInformationUpdated>();
        }
    }
}
