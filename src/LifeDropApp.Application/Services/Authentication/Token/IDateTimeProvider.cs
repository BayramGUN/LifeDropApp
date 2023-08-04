namespace LifeDropApp.Application.Services.Authentication.Token;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}