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

        public async Task<NavioDto?> GetByIdAsync(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            return _mapper.Map<NavioDto>(entity);
        }

        public async Task<NavioDto> CreateAsync(NavioDto dto)
        {
            var entity = _mapper.Map<Navio>(dto);
            var created = await _service.CreateAsync(entity);
            return _mapper.Map<NavioDto>(created);
        }

        public async Task UpdateAsync(int id, NavioDto dto)
        {
            var entity = _mapper.Map<Navio>(dto);
            await _service.UpdateAsync(id, entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
        }
    }
}
