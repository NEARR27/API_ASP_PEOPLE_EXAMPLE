using AutoMapper;
using PeopleApi.DTOS;
using PeopleApi.Models;

namespace PeopleApi.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<BeerIdDTO, Beer>();

            CreateMap<Beer,BeerDTO>()
                .ForMember(dto => dto.Id,
                            m => m.MapFrom(b => b.BeerId));

            CreateMap<BeerUpdateDTO, Beer>();

        }
    }
}
