using AutoMapper;
using LifeDropApp.Application.Common.DTOs.Responses.Donor;
using LifeDropApp.Application.Services.Donors.Interfaces;
using LifeDropApp.Application.Services.Donors.Extensions;
using LifeDropApp.Infrastructure.Repositories.Interfaces;
using LifeDropApp.Application.Common.DTOs.Requests.Donor;

namespace LifeDropApp.Application.Services.Donors;

public class DonorService : IDonorService
{
    private readonly IDonorRepository _donorRepository;
    private readonly IMapper _mapper;

    public DonorService(IDonorRepository donorRepository, IMapper mapper)
    {
        _donorRepository = donorRepository;
        _mapper = mapper;
    }

    public async Task CreateDonorAsync(CreateDonorRequest request)
    {
        if(await _donorRepository.HasUserDonor(request.UserId))
            throw new InvalidOperationException($" has information as a donor.");
        else if(request.Age < 18)
            throw new InvalidDataException("Your age must be bigger than 18!");
        var donor = request.FromCreateDonorRequestToDonor(_mapper);
        await _donorRepository.AddAsync(donor);
    }

    public async Task DeleteDonorAsync(Guid id)
    {
        if(!await _donorRepository.IsExistsAsync(id))
            throw new ArgumentNullException($"There is no Donor with {id}");
        
        await _donorRepository.RemoveAsync(id);
    }

    public async Task<IEnumerable<DonorResponse?>> GetAllDonorsAsync() 
    {
        var donors = await _donorRepository.GetAllAsync();
        return donors.FromDonorToDonorResponses(_mapper);
    }

    public async Task<DonorResponse> GetDonor(Guid id)
    {
        var donor = await _donorRepository.GetAsync(id);
        return donor!.FromDonorToDonorResponse(_mapper);
    }

    public async Task UpdateDonorAsync(UpdateDonorRequest request)
    {
        if(!await _donorRepository.IsExistsAsync(request.Id))
            throw new ArgumentNullException($"There is no Donor with {request.Id}");
        

            var donorUpdated = request.FromUpdateDonorRequestToDonor(_mapper);

            await _donorRepository.UpdateAsync(donorUpdated);
    }

    public async Task AddPointToDonor(Guid id, bool isDonate)
    {
        var donorEntity =  await _donorRepository.GetAsync(id);
        if(donorEntity is not null)
            donorEntity.Point += 10;
        await _donorRepository.UpdateAsync(donorEntity!);
    }

    public async Task<DonorResponse> GetDonorByUserId(Guid userId)
    {
        var donor = await _donorRepository.GetDonorByUserId(userId);
        return donor!.FromDonorToDonorResponse(_mapper);
    }
}