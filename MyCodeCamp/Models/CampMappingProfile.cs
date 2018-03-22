using AutoMapper;
using MyCodeCamp.Data.Entities;

namespace MyCodeCamp.Models
{
    public class CampMappingProfile : Profile
    {
        public CampMappingProfile()
        {
            CreateMap<Camp, CampModel>()
                .ForMember(c => c.Url,
                    opt => opt.ResolveUsing<CampUrlResolver>())
                .ForMember(c => c.StartDate,
                    opt => opt.MapFrom(camp => camp.EventDate))
                .ForMember(c => c.EndDate,
                    opt => opt.ResolveUsing(camp => camp.EventDate.AddDays(camp.Length - 1)))
                .ReverseMap()
                .ForMember(m => m.EventDate,
                    opt => opt.MapFrom(model => model.StartDate))
                .ForMember(m => m.Length,
                    opt => opt.ResolveUsing(model => (model.EndDate - model.StartDate).Days + 1))
                .ForMember(m => m.Location,
                    opt => opt.ResolveUsing<LocationResolver>());

            CreateMap<Speaker, SpeakerModel>()
                .ForMember(c => c.Url,
                    opt => opt.ResolveUsing<SpeakerUrlResolver>())
  
                .ReverseMap();
        }

    }
}