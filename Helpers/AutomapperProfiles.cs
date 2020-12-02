using System;
using AutoMapper;
using PharmaBackend.Models;
using PharmaBackend.DTOs;

namespace PharmaBackend.Helpers
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Medicine, MedicineDtoForList>();
            CreateMap<MedicineDtoForUpdate, Medicine>().ForMember( medDto => medDto.Id, opt => opt.Ignore());
            CreateMap<MedicineDtoForCreation, Medicine>().ReverseMap();
        }
    }
}