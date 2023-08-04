using LifeDropApp.Application.Common.DTOs.Requests;
using LifeDropApp.Application.Common.DTOs.Requests.NeedForBlood;
using LifeDropApp.Application.Common.DTOs.Responses.NeedForBlood;

namespace LifeDropApp.Application.Services.NeedForBloods.Interfaces;

public interface INeedForBloodService
{
    Task CreateNeedForBloodAsync(CreateNeedForBloodRequest createNeedFroBloodRequest);
    Task DeleteNeedForBloodAsync(Guid id);
    Task<IList<NeedForBloodResponse?>> GelAllNeedForBloodsAsync();
    Task UpdateNeedForBloodAsync(UpdateNeedForBloodRequest updateNeedForBloodRequest);
    Task DeleteNeedForBloodFromHospitalAsync(Guid hospitalId);
    Task<NeedForBloodResponse> GetNeedForBloodByIdAsync(Guid id);
    Task DecreaseQuantityNeeded(Guid id, int quantityNeeded);
    Task<IList<NeedForBloodResponse>> GetNeedForBloodsByHospitalIdAsync(Guid hospitalId);
    Task<IList<NeedForBloodResponse>> GetNeedForBloodsByBloodTypeAsync(string bloodType);
    Task<IList<NeedForBloodResponse>> GetNeedForBloodsBiggerThanZeroAsync();
}