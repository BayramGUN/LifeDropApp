using LifeDropApp.Application.Common.DTOs.Requests.Hospital;
using LifeDropApp.Application.Common.DTOs.Responses.Hospital;

namespace LifeDropApp.Application.Services.Hospitals.Interfaces;

public interface IHospitalService
{
    Task DeleteHospitalAsync(Guid id);
    Task CreateHospitalAsync(CreateHospitalRequest createHospitalRequest);
    Task<IEnumerable<HospitalResponse?>> GetAllHospitalsAsync();
    Task<HospitalResponse> GetHospital(Guid id);
    Task<HospitalResponse> GetHospitalByUserAsync(Guid userId);
    Task UpdateHospitalAsync(UpdateHospitalRequest updateHospitalRequest);
}