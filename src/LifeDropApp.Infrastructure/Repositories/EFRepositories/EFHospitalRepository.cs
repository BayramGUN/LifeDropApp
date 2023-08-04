using LifeDropApp.Domain.Entities;
using LifeDropApp.Infrastructure.DbContexts;
using LifeDropApp.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LifeDropApp.Infrastructure.Repositories.EFRepositories;

public class EFHospitalRepository : IHospitalRepository
{
    private readonly BloodDonationSqlContext _dbContext;

    public EFHospitalRepository(BloodDonationSqlContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Hospital entity)
    {
        await _dbContext.Hospitals.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Hospital?> GetHospitalByUserId(Guid Id)
    {
        return await _dbContext.Hospitals.FirstOrDefaultAsync(a => a.UserId == Id);
    }

    public async Task<IList<Hospital>> GetAllAsync() => 
        await _dbContext.Hospitals.Include(h => h.Address)
                                  .AsNoTracking()
                                  .ToListAsync();

    public async Task<Hospital?> GetAsync(Guid id) => 
        await _dbContext.Hospitals.FirstOrDefaultAsync(hospital => hospital.Id == id);

    
    public async Task<bool> IsExistsAsync(Guid id) =>
        await _dbContext.Hospitals!.AnyAsync(hospital => hospital.Id == id);

    public async Task RemoveAsync(Guid id)
    {
        var deletingHospital = await _dbContext.Hospitals.FindAsync(id);
        _dbContext.Hospitals.Remove(deletingHospital!);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Hospital entity)
    {
         _dbContext.Hospitals.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Hospital?> GetHospitalByNameAsync(string name) =>
        _dbContext.Hospitals.FirstOrDefaultAsync(hospital => hospital.Name == name);

    public async Task<bool> HasUserHospitalAsync(Guid userId) =>
        await _dbContext.Hospitals.Where(h => h.UserId == userId).AnyAsync();

    public async Task<Hospital?> GetHospitalByUserIdAsync(Guid userId) =>
        await _dbContext.Hospitals.Include(h => h.Address)
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync(h => h.UserId == userId);

     
}