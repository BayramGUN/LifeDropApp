using LifeDropApp.Domain.Entities;

namespace LifeDropApp.Infrastructure.Repositories.Interfaces;

public interface INeedForBloodRepository : IRepository<NeedForBlood>
{
    Task<IList<NeedForBlood>> GetByBloodTypeAsync(string bloodType);
    Task<IList<NeedForBlood>> GetByHospitalIdAsync(Guid hospitalId);
    Task<IList<NeedForBlood>> GetBiggerThanZeroAsync();
    Task<NeedForBlood?> GetForBloodByHospitalAndBloodType(string bloodType, Guid hospitalId);
    Task<bool> HasHospitalSameNeedAsync(string bloodType, Guid hospitalId);
    Task DeleteAllFromHospital(IList<NeedForBlood> needForBloodByHospital);
}