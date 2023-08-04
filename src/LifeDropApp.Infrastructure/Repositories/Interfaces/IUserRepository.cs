
using LifeDropApp.Domain.Entities;

namespace LifeDropApp.Infrastructure.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByPhoneAsync(string phone);
}