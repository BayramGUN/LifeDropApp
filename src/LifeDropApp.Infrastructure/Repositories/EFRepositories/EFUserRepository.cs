using LifeDropApp.Domain.Entities;
using LifeDropApp.Infrastructure.DbContexts;
using LifeDropApp.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LifeDropApp.Infrastructure.Repositories.EFRepositories;

public class EFUserRepository : IUserRepository
{
    private readonly BloodDonationSqlContext _dbContext;

    public EFUserRepository(BloodDonationSqlContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User entity)
    {
        await _dbContext.Users.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        var deletingUser = await _dbContext.Users.FindAsync(id);
        _dbContext.Users.Remove(deletingUser!);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IList<User>> GetAllAsync() => 
        await _dbContext.Users
            .Include(u => u.Admins)
            .Include(u => u.Donors)
            .Include(u => u.Hospitals)
            .ToListAsync();

    public async Task<User?> GetAsync(Guid id) => 
        await _dbContext.Users
            .Include(u => u.Admins)
            .Include(u => u.Donors)
            .Include(u => u.Hospitals)
            .FirstOrDefaultAsync(user => user.Id == id);

    public async Task<User?> GetUserByUsernameAsync(string username) => 
        await _dbContext.Users
            .Include(u => u.Admins)
            .Include(u => u.Donors)
            .Include(u => u.Hospitals)
            .SingleOrDefaultAsync(user => user.Username == username);
    public async Task<User?> GetUserByEmailAsync(string email) => 
        await _dbContext.Users.SingleOrDefaultAsync(user => user.Email! == email);
    public async Task<User?> GetUserByPhoneAsync(string phone) => 
        await _dbContext.Users.SingleOrDefaultAsync(user => user.Phone == phone);

    public async Task<bool> IsExistsAsync(Guid id) =>
        await _dbContext.Users!.AnyAsync(user => user.Id == id);

    public async Task UpdateAsync(User entity)
    {
         _dbContext.Users.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}