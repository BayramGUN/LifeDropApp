using AutoMapper;
using LifeDropApp.Application.Common.DTOs.Responses.Admin;
using LifeDropApp.Application.Services.Admins.Interfaces;
using LifeDropApp.Application.Services.Admins.Extensions;
using LifeDropApp.Infrastructure.Repositories.Interfaces;
using LifeDropApp.Application.Common.DTOs.Requests.Admin;

namespace LifeDropApp.Application.Services.Admins;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;
    private readonly IMapper _mapper;

    public AdminService(IAdminRepository adminRepository, IMapper mapper)
    {
        _adminRepository = adminRepository;
        _mapper = mapper;
    }

    public async Task CreateAdminAsync(CreateAdminRequest request)
    {
        var admin = request.FromCreateAdminRequestToAdmin(_mapper);
        await _adminRepository.AddAsync(admin);
    }

    public async Task DeleteAdminAsync(int id)
    {
        if(!await _adminRepository.IsExistsAsync(id))
            throw new ArgumentNullException($"There is no Admin with {id}");
        
        await _adminRepository.RemoveAsync(id);
    }

    public async Task<IEnumerable<AdminResponse?>> GetAllAdminsAsync() 
    {
        var admins = await _adminRepository.GetAllAsync();
        return admins.FromAdminToAdminResponses(_mapper);
    }

    public async Task<AdminResponse> GetAdmin(int id)
    {
        var admin = await _adminRepository.GetAsync(id);
        return admin!.FromAdminToAdminResponse(_mapper);
    }

    public async Task UpdateAdminAsync(UpdateAdminRequest request)
    {
        if(!await _adminRepository.IsExistsAsync(request.Id))
            throw new ArgumentNullException($"There is no Admin with {request.Id}");
        var adminUpdated = request.FromUpdateAdminRequestToAdmin(_mapper);
        await _adminRepository.UpdateAsync(adminUpdated);
    }
}