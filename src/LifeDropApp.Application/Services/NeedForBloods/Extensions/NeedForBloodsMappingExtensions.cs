using AutoMapper;
using LifeDropApp.Application.Common.DTOs.Requests.NeedForBlood;
using LifeDropApp.Application.Common.DTOs.Responses.NeedForBlood;
using LifeDropApp.Domain.Entities;

namespace LifeDropApp.Application.Services.NeedForBloods.Extensions;
public static class NeedForBloodsMappingExtensions
{
    public static IList<NeedForBloodResponse> FromNeedForBloodToNeedForBloodResponses(this IList<NeedForBlood> needForBloods, IMapper mapper) =>
        mapper.Map<IList<NeedForBloodResponse>>(needForBloods); 
    public static NeedForBloodResponse FromNeedForBloodToNeedForBloodResponse(this NeedForBlood needForBlood, IMapper mapper) =>
        mapper.Map<NeedForBloodResponse>(needForBlood); 
    public static NeedForBlood FromUpdateNeedForBloodRequestToNeedForBlood(this UpdateNeedForBloodRequest needForBloodUpdate, IMapper mapper) =>
        mapper.Map<NeedForBlood>(needForBloodUpdate);
    public static NeedForBlood FromCreateNeedForBloodRequestToNeedForBlood(this CreateNeedForBloodRequest needForBloodCreate, IMapper mapper) =>
        mapper.Map<NeedForBlood>(needForBloodCreate);
}

