using AutoMapper;
using LifeDropApp.Application.Common.DTOs.Requests.Hospital;
using LifeDropApp.Application.Common.DTOs.Responses.Hospital;
using LifeDropApp.Domain.Entities;

namespace LifeDropApp.Application.Services.Hospitals.Extensions;
public static class HospitalsMappingExtensions
{
    public static IEnumerable<HospitalResponse> FromHospitalToHospitalResponses(this IList<Hospital> hospitals, IMapper mapper) =>
        mapper.Map<IEnumerable<HospitalResponse>>(hospitals); 
    public static HospitalResponse FromHospitalToHospitalResponse(this Hospital hospital, IMapper mapper) =>
        mapper.Map<HospitalResponse>(hospital); 
    public static Hospital FromUpdateHospitalRequestToHospital(this UpdateHospitalRequest hospitalUpdate, IMapper mapper) =>
        mapper.Map<Hospital>(hospitalUpdate);
    public static Hospital FromCreateHospitalRequestToHospital(this CreateHospitalRequest hospitalCreate, IMapper mapper) =>
        mapper.Map<Hospital>(hospitalCreate);
}

