using SEBO.API.Domain.Entities.Base;
using SEBO.API.Domain.Interface.Repository.Base;

namespace SEBO.Services
{
    public class BaseService<T> where T : BaseEntity
    {
        protected readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<IEnumerable<T>> Get() => (await _repository.GetAll())!;

        public virtual async Task<T> GetById(int id) => (await _repository.GetById(id))!;

        public virtual async Task<T> Add(T entity) => (await _repository.Add(entity))!;
        public virtual async Task AddRange(params T[] entities) => await _repository.AddRange(entities);
        public virtual async Task<T> Update(T entity) => (await _repository.Update(entity))!;
        public virtual async Task UpdateRange(params T[] entities) => await _repository.UpdateRange(entities);

        public virtual async Task<bool> Delete(int id)
        {
            await _repository.DeleteById(id);
            return true;
        }
    }
}