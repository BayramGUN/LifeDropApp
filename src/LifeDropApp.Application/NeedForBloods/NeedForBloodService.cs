using AutoMapper;
using LifeDropApp.Application.Common.DTOs.Requests.NeedForBlood;
using LifeDropApp.Application.Common.DTOs.Responses.NeedForBlood;
using LifeDropApp.Application.Services.NeedForBloods.Extensions;
using LifeDropApp.Application.Services.NeedForBloods.Interfaces;
using LifeDropApp.Infrastructure.Repositories.Interfaces;

namespace LifeDropApp.Application.Services.NeedForBloods;

public class NeedForBloodService : INeedForBloodService
{
    private readonly INeedForBloodRepository _needForBloodRepository;
    private readonly IMapper _mapper;

    public NeedForBloodService(INeedForBloodRepository needForBloodRepository, IMapper mapper)
    {
        _needForBloodRepository = needForBloodRepository;
        _mapper = mapper;
    }
    public async Task CreateNeedForBloodAsync(CreateNeedForBloodRequest request)
    {
        if(!await _needForBloodRepository.HasHospitalSameNeedAsync(request.BloodType, request.HospitalId))
        {
            var createdNeedForBlood = request.FromCreateNeedForBloodRequestToNeedForBlood(_mapper);
            await _needForBloodRepository.AddAsync(createdNeedForBlood);
        }
        else 
        {
            var needForBlood = await _needForBloodRepository
                                                .GetForBloodByHospitalAndBloodType(
                                                    request.BloodType,
                                                    request.HospitalId);
            needForBlood!.QuantityNeeded += request.QuantityNeeded;
            await _needForBloodRepository.UpdateAsync(needForBlood!);
        }
    }

    public async Task DecreaseQuantityNeeded(Guid id, int quantityNeeded)
    {
        var needForBlood = await _needForBloodRepository.GetAsync(id);
        needForBlood!.QuantityNeeded -= quantityNeeded;
        if(needForBlood!.QuantityNeeded < 0)
            throw new InvalidOperationException($"The quantity of need can not be smaller than zero. The value of need: {needForBlood.QuantityNeeded}");
        await _needForBloodRepository.UpdateAsync(needForBlood); 
    }

    public async Task DeleteNeedForBloodAsync(Guid id)
    {
        if(await _needForBloodRepository.IsExistsAsync(id))
            await _needForBloodRepository.RemoveAsync(id);
    }

    public async Task DeleteNeedForBloodFromHospitalAsync(Guid hospitalId)
    {
        var needForBloodsByHospital = await _needForBloodRepository.GetByHospitalIdAsync(hospitalId);
        await _needForBloodRepository.DeleteAllFromHospital(needForBloodsByHospital);
    }

    public async Task<IList<NeedForBloodResponse?>> GelAllNeedForBloodsAsync()
    {
        var needForBloods = await _needForBloodRepository.GetAllAsync();
        return needForBloods.FromNeedForBloodToNeedForBloodResponses(_mapper)!;
    }

    public async Task<NeedForBloodResponse> GetNeedForBloodByIdAsync(Guid id)
    {
        var needForBlood = await _needForBloodRepository.GetAsync(id);
        return needForBlood!.FromNeedForBloodToNeedForBloodResponse(_mapper);
    }

    public async Task<IList<NeedForBloodResponse>> GetNeedForBloodsBiggerThanZeroAsync()
    {
        var needForBloods = await _needForBloodRepository.GetBiggerThanZeroAsync();
        return needForBloods.FromNeedForBloodToNeedForBloodResponses(_mapper)!;
    }

    public async Task<IList<NeedForBloodResponse>> GetNeedForBloodsByBloodTypeAsync(string bloodType)
    {
        var needForBloods = await _needForBloodRepository.GetByBloodTypeAsync(bloodType);
        return needForBloods.FromNeedForBloodToNeedForBloodResponses(_mapper)!;
    }

    public async Task<IList<NeedForBloodResponse>> GetNeedForBloodsByHospitalIdAsync(Guid hospitalId)
    {
        var needForBloods = await _needForBloodRepository.GetByHospitalIdAsync(hospitalId);
        return needForBloods.FromNeedForBloodToNeedForBloodResponses(_mapper)!;
    }

    public async Task UpdateNeedForBloodAsync(UpdateNeedForBloodRequest updateNeedForBloodRequest)
    {
        var updatedNeedForBlood = updateNeedForBloodRequest.FromUpdateNeedForBloodRequestToNeedForBlood(_mapper);
        await _needForBloodRepository.UpdateAsync(updatedNeedForBlood);
    }
}