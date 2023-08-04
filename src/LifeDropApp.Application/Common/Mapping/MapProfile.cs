using AutoMapper;
using LifeDropApp.Application.Common.DTOs.Requests.Address;
using LifeDropApp.Application.Common.DTOs.Requests.Admin;
using LifeDropApp.Application.Common.DTOs.Requests.Authentication;
using LifeDropApp.Application.Common.DTOs.Requests.Donor;
using LifeDropApp.Application.Common.DTOs.Requests.Hospital;
using LifeDropApp.Application.Common.DTOs.Requests.NeedForBlood;
using LifeDropApp.Application.Common.DTOs.Requests.User;
using LifeDropApp.Application.Common.DTOs.Responses.Admin;
using LifeDropApp.Application.Common.DTOs.Responses.Donor;
using LifeDropApp.Application.Common.DTOs.Responses.Hospital;
using LifeDropApp.Application.Common.DTOs.Responses.NeedForBlood;
using LifeDropApp.Application.Common.DTOs.Responses.User;
using LifeDropApp.Application.Services.Authentication.Extensions;
using LifeDropApp.Domain.Entities;

namespace LifeDropApp.Application.Common.Mapping;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<NeedForBlood, NeedForBloodResponse>()
            .ForMember(dest => dest.Hospital, opt => opt.MapFrom(src => src.Hospital));
        CreateMap<CreateNeedForBloodRequest, NeedForBlood>();
        CreateMap<UpdateNeedForBloodRequest, NeedForBlood>();

        CreateMap<CreateUserRequest, User>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password.GetPasswordHash()))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToLower()));
        CreateMap<UpdateUserRequest, User>();
        
        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.AdminName, opt => opt.MapFrom(src => src.Admins.Select(a => a.Name).ToList()))
            .ForMember(dest => dest.DonorName, opt => opt.MapFrom(src => src.Donors.Select(a => a.Firstname).ToList()))
            .ForMember(dest => dest.HospitalName, opt => opt.MapFrom(src => src.Hospitals.Select(a => a.Name).ToList()));

        CreateMap<Admin, AdminResponse>();
        CreateMap<CreateAdminRequest, Admin>();
        CreateMap<UpdateAdminRequest, Admin>();
       
        CreateMap<Hospital, HospitalResponse>()
            .ForMember(dest => dest.Address, opt => 
                opt.MapFrom(src => $"{src.Address!.Street} {src.Address.City}/{src.Address.Country} {src.Address.ZipCode}"));
        CreateMap<CreateAddressRequest, Address>().ReverseMap();
        CreateMap<CreateHospitalRequest, Hospital>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
        CreateMap<UpdateHospitalRequest, Hospital>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

        CreateMap<Donor, DonorResponse>()
            .ForMember(dest => dest.Address, opt => 
                opt.MapFrom(src => $"{src.Address!.Street} {src.Address.City}/{src.Address.Country} {src.Address.ZipCode}")
            )
            .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => $"{src.Firstname} {src.Lastname}"));
        CreateMap<CreateDonorRequest, Donor>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
        CreateMap<UpdateDonorRequest, Donor>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
        CreateMap<UpdateAddressRequest, Address>().ReverseMap();

        
    }
}
