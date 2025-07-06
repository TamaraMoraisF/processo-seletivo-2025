using AutoMapper;
using Seals.Duv.Application.DTOs;
using Seals.Duv.Domain.Entities;

namespace Seals.Duv.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Duv, DuvDto>().ReverseMap();
            CreateMap<Navio, NavioDto>().ReverseMap();
            CreateMap<Passageiro, PassageiroDto>().ReverseMap();
            CreateMap<CreatePassageiroDto, Passageiro>();
            CreateMap<UpdatePassageiroDto, Passageiro>();
        }
    }
}
