using AutoMapper;
using Seals.Duv.Application.DTOs;
using Seals.Duv.Application.Interfaces;
using Seals.Duv.Domain.Interfaces;

namespace Seals.Duv.Application.Applications
{
    public class DuvApplication : IDuvApplication
    {
        private readonly IDuvService _service;
        private readonly IMapper _mapper;

        public DuvApplication(IDuvService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DuvDto>> GetAllAsync()
        {
            var list = await _service.GetAllAsync();
            return _mapper.Map<IEnumerable<DuvDto>>(list);
        }

        public async Task<DuvDto?> GetByIdAsync(int id)
        {
            var duv = await _service.GetByIdAsync(id);
            return _mapper.Map<DuvDto?>(duv);
        }

        public async Task<DuvDto> CreateAsync(DuvDto dto)
        {
            var entity = _mapper.Map<Domain.Entities.Duv>(dto);
            var created = await _service.CreateAsync(entity);
            return _mapper.Map<DuvDto>(created);
        }

        public async Task UpdateAsync(int id, DuvDto dto)
        {
            var entity = _mapper.Map<Domain.Entities.Duv>(dto);
            await _service.UpdateAsync(id, entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
        }
    }
}
