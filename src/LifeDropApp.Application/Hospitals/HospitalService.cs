using AutoMapper;
using LifeDropApp.Application.Common.DTOs.Responses.Hospital;
using LifeDropApp.Application.Services.Hospitals.Interfaces;
using LifeDropApp.Application.Services.Hospitals.Extensions;
using LifeDropApp.Infrastructure.Repositories.Interfaces;
using LifeDropApp.Application.Common.DTOs.Requests.Hospital;

namespace LifeDropApp.Application.Services.Hospitals;

public class HospitalService : IHospitalService
{
    private readonly IHospitalRepository _hospitalRepository;
    private readonly IMapper _mapper;

    public HospitalService(IHospitalRepository hospitalRepository, IMapper mapper)
    {
        _hospitalRepository = hospitalRepository;
        _mapper = mapper;
    }

    public async Task CreateHospitalAsync(CreateHospitalRequest request)
    {
        /* if(await _hospitalRepository.HasUserHospitalAsync(request.UserId))
            throw new InvalidOperationException(" has information as hospital"); */
        var hospital = request.FromCreateHospitalRequestToHospital(_mapper);
        await _hospitalRepository.AddAsync(hospital);
    }

    public async Task DeleteHospitalAsync(Guid id)
    {
        if(!await _hospitalRepository.IsExistsAsync(id))
            throw new ArgumentNullException($"There is no Hospital with {id}");
        
        await _hospitalRepository.RemoveAsync(id);
    }

    public async Task<IEnumerable<HospitalResponse?>> GetAllHospitalsAsync() 
    {
        var hospitals = await _hospitalRepository.GetAllAsync();
        return hospitals.FromHospitalToHospitalResponses(_mapper);
    }

    public async Task<HospitalResponse> GetHospital(Guid id)
    {
        var hospital = await _hospitalRepository.GetAsync(id);
        return hospital!.FromHospitalToHospitalResponse(_mapper);
    }

    public async Task<HospitalResponse> GetHospitalByUserAsync(Guid userId)
    {
        var hospital = await _hospitalRepository.GetHospitalByUserIdAsync(userId);
        return hospital!.FromHospitalToHospitalResponse(_mapper);
    }

    public async Task UpdateHospitalAsync(UpdateHospitalRequest request)
    {
        if(!await _hospitalRepository.IsExistsAsync(request.Id))
            throw new ArgumentNullException($"There is no Hospital with {request.Id}");
        var hospitalUpdated = request.FromUpdateHospitalRequestToHospital(_mapper);
        await _hospitalRepository.UpdateAsync(hospitalUpdated);
    }
}