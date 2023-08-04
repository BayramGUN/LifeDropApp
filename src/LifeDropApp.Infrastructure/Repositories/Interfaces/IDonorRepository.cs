using LifeDropApp.Domain.Entities;

namespace LifeDropApp.Infrastructure.Repositories.Interfaces;

public interface IDonorRepository : IRepository<Donor>
{
    Task<IList<Donor>> GetDonorsByNameAsync(string name);
    Task<Donor?> GetDonorByUserId(Guid userId);
    Task<bool> HasUserDonor(Guid userId);
}