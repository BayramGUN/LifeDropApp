using LifeDropApp.Domain.Entities;

namespace LifeDropApp.Infrastructure.Repositories.Interfaces;

public interface IHospitalRepository : IRepository<Hospital>
{
    Task<Hospital?> GetHospitalByNameAsync(string name);
    Task<bool> HasUserHospitalAsync(Guid userId);
    Task<Hospital?> GetHospitalByUserIdAsync(Guid userId);
}