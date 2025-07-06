using AutoMapper;
using Seals.Duv.Application.DTOs;
using Seals.Duv.Application.Interfaces;
using Seals.Duv.Domain.Entities;
using Seals.Duv.Domain.Interfaces;

namespace Seals.Duv.Application.Applications
{
    public class NavioApplication : INavioApplication
    {
        private readonly INavioService _service;
        private readonly IMapper _mapper;

        public NavioApplication(INavioService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NavioDto>> GetAllAsync()
        {
            var entities = await _service.GetAllAsync();
            return _mapper.Map<IEnumerable<NavioDto>>(entities);
        }

        public async Task<NavioDto?> GetByGuidAsync(Guid guid)
        {
            var entity = await _service.GetByGuidAsync(guid);
            return _mapper.Map<NavioDto>(entity);
        }

        public async Task<NavioDto> CreateAsync(CreateNavioDto dto)
        {
            var entity = _mapper.Map<Navio>(dto);
            if (entity.NavioGuid == Guid.Empty)
                entity.NavioGuid = Guid.NewGuid();

            var created = await _service.CreateAsync(entity);
            return _mapper.Map<NavioDto>(created);
        }

        public async Task UpdateByGuidAsync(Guid guid, UpdateNavioDto dto)
        {
            var entity = _mapper.Map<Navio>(dto);
            await _service.UpdateByGuidAsync(guid, entity);
        }

        public Task DeleteByGuidAsync(Guid guid) => _service.DeleteByGuidAsync(guid);
    }
}
