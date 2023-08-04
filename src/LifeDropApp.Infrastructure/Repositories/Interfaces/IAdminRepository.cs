using LifeDropApp.Domain.Entities;

namespace LifeDropApp.Infrastructure.Repositories.Interfaces;

public interface IAdminRepository
{
    Task RemoveAsync(int id);
    Task<Admin?> GetAsync(int id);
    Task<IList<Admin>> GetAllAsync();
    Task<bool> IsExistsAsync(int id);
    Task UpdateAsync(Admin entity);
    Task<Admin?> GetAdminByUserId(Guid Id);
    Task AddAsync(Admin entity);
}