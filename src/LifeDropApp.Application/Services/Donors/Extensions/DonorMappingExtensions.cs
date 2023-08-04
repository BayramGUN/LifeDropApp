using AutoMapper;
using LifeDropApp.Application.Common.DTOs.Requests.Donor;
using LifeDropApp.Application.Common.DTOs.Responses.Donor;
using LifeDropApp.Domain.Entities;

namespace LifeDropApp.Application.Services.Donors.Extensions;
public static class DonorsMappingExtensions
{
    public static IEnumerable<DonorResponse> FromDonorToDonorResponses(this IList<Donor> donors, IMapper mapper) =>
        mapper.Map<IEnumerable<DonorResponse>>(donors); 
    public static DonorResponse FromDonorToDonorResponse(this Donor donor, IMapper mapper) =>
        mapper.Map<DonorResponse>(donor); 
    public static Donor FromUpdateDonorRequestToDonor(this UpdateDonorRequest donorUpdate, IMapper mapper) =>
        mapper.Map<Donor>(donorUpdate);
    public static Donor FromCreateDonorRequestToDonor(this CreateDonorRequest donorCreate, IMapper mapper) =>
        mapper.Map<Donor>(donorCreate);
}

