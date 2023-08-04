using LifeDropApp.Application.Services.Authentication.Token;

namespace LifeDropApp.Infrastructure.Authentication;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}