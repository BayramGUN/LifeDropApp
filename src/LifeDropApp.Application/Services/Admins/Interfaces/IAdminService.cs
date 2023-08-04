using LifeDropApp.Application.Common.DTOs.Requests;
using LifeDropApp.Application.Common.DTOs.Requests.Admin;
using LifeDropApp.Application.Common.DTOs.Responses.Admin;

namespace LifeDropApp.Application.Services.Admins.Interfaces;

public interface IAdminService
{
    Task DeleteAdminAsync(int id);
    Task CreateAdminAsync(CreateAdminRequest createAdminRequest);
    Task<IEnumerable<AdminResponse?>> GetAllAdminsAsync();
    Task<AdminResponse> GetAdmin(int id);
    Task UpdateAdminAsync(UpdateAdminRequest updateAdminRequest);
}