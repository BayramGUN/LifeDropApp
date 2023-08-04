using LifeDropApp.Application.Common.DTOs.Requests.Donor;
using LifeDropApp.Application.Common.DTOs.Responses.Donor;

namespace LifeDropApp.Application.Services.Donors.Interfaces;

public interface IDonorService
{
    Task DeleteDonorAsync(Guid id);
    Task CreateDonorAsync(CreateDonorRequest createDonorRequest);
    Task<IEnumerable<DonorResponse?>> GetAllDonorsAsync();
    Task<DonorResponse> GetDonor(Guid id);
    Task UpdateDonorAsync(UpdateDonorRequest updateDonorRequest);
    Task AddPointToDonor(Guid id, bool isDonate);
    Task<DonorResponse> GetDonorByUserId(Guid userId);
}