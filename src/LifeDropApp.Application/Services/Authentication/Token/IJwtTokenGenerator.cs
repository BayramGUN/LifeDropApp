
using LifeDropApp.Domain.Entities;

namespace LifeDropApp.Application.Services.Authentication.Token;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}