using Seals.Duv.Domain.Entities;
using Seals.Duv.Domain.Interfaces;

namespace Seals.Duv.Application.Services
{
    public class PassageiroService(IPassageiroRepository repository) : IPassageiroService
    {
        private readonly IPassageiroRepository _repository = repository;

        public Task<IEnumerable<Passageiro>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Passageiro?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<Passageiro> CreateAsync(Passageiro passageiro)
        {
            return _repository.AddAsync(passageiro).ContinueWith(_ => passageiro);
        }
        public Task UpdateAsync(int id, Passageiro passageiro)
        {
            passageiro.Id = id;
            return _repository.UpdateAsync(passageiro);
        }
        public Task DeleteAsync(int id)
        {
            var passageiro = _repository.GetByIdAsync(id).Result;
            return _repository.DeleteAsync(passageiro);
        }
    }
}