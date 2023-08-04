using LifeDropApp.Domain.Entities;
using LifeDropApp.Infrastructure.DbContexts;
using LifeDropApp.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LifeDropApp.Infrastructure.Repositories.EFRepositories;

public class EFNeedForBloodRepository : INeedForBloodRepository
{
    private readonly BloodDonationSqlContext _dbContext;

    public EFNeedForBloodRepository(BloodDonationSqlContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(NeedForBlood entity)
    {
        await _dbContext.NeedForBloods.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IList<NeedForBlood>> GetAllAsync() => 
        await _dbContext.NeedForBloods.Include(nfb => nfb.Hospital)
                                      .ThenInclude(h => h.Address)
                                      .AsNoTracking()
                                      .ToListAsync();

    public async Task<NeedForBlood?> GetAsync(Guid id) =>
        await _dbContext.NeedForBloods.Include(nfb => nfb.Hospital)
                                      .ThenInclude(h => h.Address)
                                      .FirstAsync(nfb => nfb.Id == id);

    public async Task<IList<NeedForBlood>> GetBiggerThanZeroAsync() =>
        await _dbContext.NeedForBloods.Include(nfb => nfb.Hospital)
                                      .ThenInclude(h => h.Address)
                                      .Where(nfb => nfb.QuantityNeeded > 0)
                                      .ToListAsync();

    public async Task<IList<NeedForBlood>> GetByBloodTypeAsync(string bloodType) =>
        await _dbContext.NeedForBloods.Include(nfb => nfb.Hospital)
                                      .ThenInclude(h => h.Address)
                                      .Where(nfb => nfb.BloodType == bloodType && nfb.QuantityNeeded > 0)
                                      .ToListAsync();

    public async Task<IList<NeedForBlood>> GetByHospitalIdAsync(Guid hospitalId) =>
        await _dbContext.NeedForBloods.Include(nfb => nfb.Hospital)
                                      .ThenInclude(h => h.Address)
                                      .AsNoTrackingWithIdentityResolution()
                                      .Where(nfb => nfb.HospitalId == hospitalId)
                                      .ToListAsync();

    public async Task<bool> IsExistsAsync(Guid id) =>
        await _dbContext.NeedForBloods.AsNoTrackingWithIdentityResolution()
                                      .AnyAsync(nfb => nfb.Id == id);
    public async Task<bool> HasHospitalSameNeedAsync(string bloodType, Guid hospitalId) =>
        await _dbContext.NeedForBloods.AsNoTrackingWithIdentityResolution()
                                      .AnyAsync(nfb => (nfb.BloodType == bloodType && nfb.HospitalId == hospitalId));

    public async Task RemoveAsync(Guid id)
    {
        var needForBloodForDelete = await _dbContext.NeedForBloods.FindAsync(id);
        _dbContext.NeedForBloods.Remove(needForBloodForDelete!);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(NeedForBlood entity)
    {
        _dbContext.NeedForBloods.Update(entity!);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<NeedForBlood?> GetForBloodByHospitalAndBloodType(string bloodType, Guid hospitalId) =>
        await _dbContext.NeedForBloods.Include(nfb => nfb.Hospital)
                                      .ThenInclude(h => h.Address)
                                      .FirstOrDefaultAsync(nfb => (nfb.BloodType == bloodType && nfb.HospitalId == hospitalId));

    public async Task DeleteAllFromHospital(IList<NeedForBlood> needForBloodByHospital)
    {
        _dbContext.NeedForBloods.RemoveRange(needForBloodByHospital);
        await _dbContext.SaveChangesAsync();
    }
}