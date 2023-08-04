namespace LifeDropApp.Application.Common.DTOs.Responses.Admin;

public class AdminResponse
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; } = null!;
}
