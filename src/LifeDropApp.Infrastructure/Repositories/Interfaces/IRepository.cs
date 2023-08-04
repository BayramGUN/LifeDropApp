using LifeDropApp.Domain.Entities;

namespace LifeDropApp.Infrastructure.Repositories.Interfaces;
public interface IRepository<T> where T : 
    class, IEntity, new()
{
    Task<T?> GetAsync(Guid id);
    Task<IList<T>> GetAllAsync();

    Task AddAsync(T entity);
    Task RemoveAsync(Guid id);
    Task UpdateAsync(T entity);

    Task<bool> IsExistsAsync(Guid id);
}

