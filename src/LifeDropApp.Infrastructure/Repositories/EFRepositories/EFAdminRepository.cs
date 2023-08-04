using LifeDropApp.Domain.Entities;
using LifeDropApp.Infrastructure.DbContexts;
using LifeDropApp.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LifeDropApp.Infrastructure.Repositories.EFRepositories;

public class EFAdminRepository : IAdminRepository
{
    private readonly BloodDonationSqlContext _dbContext;

    public EFAdminRepository(BloodDonationSqlContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Admin entity)
    {
        await _dbContext.Admins.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Admin?> GetAdminByUserId(Guid Id)
    {
        return await _dbContext.Admins.FirstOrDefaultAsync(a => a.UserId == Id);
    }

    public async Task<IList<Admin>> GetAllAsync() => 
        await _dbContext.Admins.AsNoTracking().ToListAsync();

    public async Task<Admin?> GetAsync(int id) => 
        await _dbContext.Admins.FirstOrDefaultAsync(admin => admin.Id == id);


    public async Task<bool> IsExistsAsync(int id) =>
        await _dbContext.Admins!.AnyAsync(admin => admin.Id == id);

    public async Task RemoveAsync(int id)
    {
        var deletingAdmin = await _dbContext.Admins.FindAsync(id);
        _dbContext.Admins.Remove(deletingAdmin!);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Admin entity)
    {
         _dbContext.Admins.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}