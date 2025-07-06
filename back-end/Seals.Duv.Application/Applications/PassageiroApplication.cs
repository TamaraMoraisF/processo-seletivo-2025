using AutoMapper;
using Seals.Duv.Application.DTOs;
using Seals.Duv.Application.Interfaces;
using Seals.Duv.Domain.Entities;
using Seals.Duv.Domain.Interfaces;

namespace Seals.Duv.Application.Applications
{
    public class PassageiroApplication(
        IPassageiroService service,
        IDuvService duvService,
        IMapper mapper) : IPassageiroApplication
    {
        private readonly IPassageiroService _service = service;
        private readonly IDuvService _duvService = duvService;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<PassageiroDto>> GetAllAsync()
        {
            var items = await _service.GetAllAsync();
            return _mapper.Map<IEnumerable<PassageiroDto>>(items);
        }

        public async Task<PassageiroDto?> GetByGuidAsync(Guid guid)
        {
            var result = await _service.GetByGuidAsync(guid);
            return result is not null ? _mapper.Map<PassageiroDto>(result) : null;
        }

        public async Task<PassageiroDto?> CreateAsync(CreatePassageiroDto dto)
        {
            var duv = await _duvService.GetByGuidAsync(dto.DuvGuid);
            if (duv is null)
                return null;

            var entity = _mapper.Map<Passageiro>(dto);
            entity.DuvId = duv.Id;

            var created = await _service.CreateAsync(entity);
            return _mapper.Map<PassageiroDto>(created);
        }

        public async Task<bool> UpdateByGuidAsync(Guid guid, UpdatePassageiroDto dto)
        {
            var duv = await _duvService.GetByGuidAsync(dto.DuvGuid);
            if (duv is null)
                return false;

            var entity = _mapper.Map<Passageiro>(dto);
            entity.DuvId = duv.Id;

            await _service.UpdateByGuidAsync(guid, entity);
            return true;
        }

        public Task DeleteByGuidAsync(Guid guid) => _service.DeleteByGuidAsync(guid);
    }
}
