using AutoMapper;
using Seals.Duv.Application.DTOs;
using Seals.Duv.Application.Interfaces;
using Seals.Duv.Domain.Entities;
using Seals.Duv.Domain.Interfaces;

namespace Seals.Duv.Application.Applications
{
    public class PassageiroApplication(IPassageiroService service, IMapper mapper) : IPassageiroApplication
    {
        private readonly IPassageiroService _service = service;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<PassageiroDto>> GetAllAsync()
        {
            var items = await _service.GetAllAsync();
            return _mapper.Map<IEnumerable<PassageiroDto>>(items);
        }

        public async Task<PassageiroDto?> GetByIdAsync(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result is not null ? _mapper.Map<PassageiroDto>(result) : null;
        }

        public async Task<PassageiroDto> CreateAsync(PassageiroDto dto)
        {
            var entity = _mapper.Map<Passageiro>(dto);
            var created = await _service.CreateAsync(entity);
            return _mapper.Map<PassageiroDto>(created);
        }

        public async Task UpdateAsync(int id, PassageiroDto dto)
        {
            var entity = _mapper.Map<Passageiro>(dto);
            await _service.UpdateAsync(id, entity);
        }

        public Task DeleteAsync(int id) => _service.DeleteAsync(id);
    }
}
