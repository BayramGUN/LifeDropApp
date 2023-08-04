using AutoMapper;
using LifeDropApp.Application.Common.DTOs.Requests.Admin;
using LifeDropApp.Application.Common.DTOs.Responses.Admin;
using LifeDropApp.Domain.Entities;

namespace LifeDropApp.Application.Services.Admins.Extensions;
public static class AdminsMappingExtensions
{
    public static IEnumerable<AdminResponse> FromAdminToAdminResponses(this IList<Admin> admins, IMapper mapper) =>
        mapper.Map<IEnumerable<AdminResponse>>(admins); 
    public static AdminResponse FromAdminToAdminResponse(this Admin admin, IMapper mapper) =>
        mapper.Map<AdminResponse>(admin); 
    public static Admin FromUpdateAdminRequestToAdmin(this UpdateAdminRequest adminUpdate, IMapper mapper) =>
        mapper.Map<Admin>(adminUpdate);
    public static Admin FromCreateAdminRequestToAdmin(this CreateAdminRequest adminCreate, IMapper mapper) =>
        mapper.Map<Admin>(adminCreate);
}

