using AutoMapper;
using Seals.Duv.Application.DTOs;
using Seals.Duv.Domain.Entities;

namespace Seals.Duv.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Duv, DuvDto>();
            CreateMap<CreateDuvDto, Domain.Entities.Duv>();
            CreateMap<UpdateDuvDto, Domain.Entities.Duv>();
            CreateMap<Navio, NavioDto>();
            CreateMap<CreateNavioDto, Navio>();
            CreateMap<UpdateNavioDto, Navio>();
            CreateMap<Passageiro, PassageiroDto>();
            CreateMap<CreatePassageiroDto, Passageiro>();
            CreateMap<UpdatePassageiroDto, Passageiro>();
            CreateMap<PassageiroDuvDto, Passageiro>();
        }
    }
}
