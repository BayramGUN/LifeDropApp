using LifeDropApp.Domain.Entities;
using LifeDropApp.Infrastructure.DbContexts;
using LifeDropApp.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LifeDropApp.Infrastructure.Repositories.EFRepositories;

public class EFDonorRepository : IDonorRepository
{
    private readonly BloodDonationSqlContext _dbContext;

    public EFDonorRepository(BloodDonationSqlContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Donor entity)
    {
        await _dbContext.Donors.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Donor?> GetDonorByUserId(Guid userId) =>
        await _dbContext.Donors.Include(d => d.Address)
                               .FirstOrDefaultAsync(a => a.UserId == userId);
    public async Task<bool> HasUserDonor(Guid userId) =>
        await _dbContext.Donors.AnyAsync(a => a.UserId == userId);

    public async Task<IList<Donor>> GetAllAsync() => 
        await _dbContext.Donors.Include(d => d.Address)
                               .AsNoTracking()
                               .ToListAsync();

    public async Task<Donor?> GetAsync(Guid id) => 
        await _dbContext.Donors.AsNoTrackingWithIdentityResolution()
                               .Include(d => d.Address)
                               .FirstOrDefaultAsync(donor => donor.Id == id);


    public async Task<bool> IsExistsAsync(Guid id) =>
        await _dbContext.Donors!.AsNoTrackingWithIdentityResolution()
                                .AnyAsync(donor => donor.Id == id);

    public async Task RemoveAsync(Guid id)
    {
        var deletingDonor = await _dbContext.Donors.FindAsync(id);
        _dbContext.Donors.Remove(deletingDonor!);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Donor entity)
    {
        _dbContext.Donors.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IList<Donor>> GetDonorsByNameAsync(string name) =>
        await _dbContext.Donors.Include(d => d.Address)
                               .Where(donor => donor.Firstname == name)
                               .ToListAsync();
}