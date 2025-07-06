using AutoMapper;
using Seals.Duv.Application.DTOs;
using Seals.Duv.Application.Interfaces;
using Seals.Duv.Domain.Interfaces;

namespace Seals.Duv.Application.Applications
{
    public class DuvApplication(IDuvService duvService, INavioService navioService, IMapper mapper) : IDuvApplication
    {
        private readonly IDuvService _duvService = duvService;
        private readonly INavioService _navioService = navioService;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<DuvDto>> GetAllAsync()
        {
            var list = await _duvService.GetAllAsync();
            return _mapper.Map<IEnumerable<DuvDto>>(list);
        }

        public async Task<DuvDto?> GetByGuidAsync(Guid guid)
        {
            var duv = await _duvService.GetByGuidAsync(guid);
            return _mapper.Map<DuvDto?>(duv);
        }

        public async Task<DuvDto?> CreateAsync(CreateDuvDto dto)
        {
            var navio = await _navioService.GetByGuidAsync(dto.NavioGuid);
            if (navio is null)
                return null;

            var entity = _mapper.Map<Domain.Entities.Duv>(dto);
            entity.NavioId = navio.Id;

            var created = await _duvService.CreateAsync(entity);
            return _mapper.Map<DuvDto>(created);
        }

        public async Task<bool> UpdateByGuidAsync(Guid guid, UpdateDuvDto dto)
        {
            var navio = await _navioService.GetByGuidAsync(dto.NavioGuid);
            if (navio is null)
                return false;

            var entity = _mapper.Map<Domain.Entities.Duv>(dto);
            entity.NavioId = navio.Id;

            await _duvService.UpdateByGuidAsync(guid, entity);
            return true;
        }

        public Task DeleteByGuidAsync(Guid guid) => _duvService.DeleteByGuidAsync(guid);
    }
}
