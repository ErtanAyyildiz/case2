using AutoMapper;
using Case.Models;
using Case.Models.DTOs;

namespace Case.WebUI.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Person, PersonCreateDTO>().ReverseMap();
        }
    }
}
