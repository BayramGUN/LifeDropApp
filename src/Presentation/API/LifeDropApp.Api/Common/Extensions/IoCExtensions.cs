using System.Text;
using LifeDropApp.Application.Authentication.Login;
using LifeDropApp.Application.Authentication.Register;
using LifeDropApp.Application.Common.Mapping;
using LifeDropApp.Application.Services.Admins;
using LifeDropApp.Application.Services.Admins.Interfaces;
using LifeDropApp.Application.Services.Authentication.Interfaces;
using LifeDropApp.Application.Services.Authentication.Token;
using LifeDropApp.Application.Services.Donors;
using LifeDropApp.Application.Services.Donors.Interfaces;
using LifeDropApp.Application.Services.Hospitals;
using LifeDropApp.Application.Services.Hospitals.Interfaces;
using LifeDropApp.Application.Services.NeedForBloods;
using LifeDropApp.Application.Services.NeedForBloods.Interfaces;
using LifeDropApp.Application.Services.Users;
using LifeDropApp.Application.Services.Users.Interfaces;
using LifeDropApp.Infrastructure.Authentication;
using LifeDropApp.Infrastructure.DbContexts;
using LifeDropApp.Infrastructure.Repositories.EFRepositories;
using LifeDropApp.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public static class IoCExtensions
{
    public static IServiceCollection AddDependencyInjections(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAutoMapper(typeof(MapProfile));
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, EFUserRepository>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAdminRepository, EFAdminRepository>();
        services.AddScoped<IHospitalService, HospitalService>();
        services.AddScoped<IHospitalRepository, EFHospitalRepository>();
        services.AddScoped<IDonorService, DonorService>();
        services.AddScoped<IDonorRepository, EFDonorRepository>();
        services.AddScoped<INeedForBloodService, NeedForBloodService>();
        services.AddScoped<INeedForBloodRepository, EFNeedForBloodRepository>();

        //IoC
        services.AddDbContext<BloodDonationSqlContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SQLServer"))
        );

        services.AddAuth(configuration);
        return services;
    }
    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });
        services.AddAuthorization(options => {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
            options.AddPolicy("HospitalOnly", policy => policy.RequireRole("hospital"));
            options.AddPolicy("DonorOnly", policy => policy.RequireRole("donor"));
        });
            
        return services;
    }
}