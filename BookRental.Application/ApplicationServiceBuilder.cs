using System.Reflection;
using System.Text;
using Application.Jwt;
using Application.Service;
using BookRental.Domain;
using BookRental.Domain.Entities;
using BookRental.Domain.Interfaces;
using BookRental.Domain.Interfaces.Repositories;
using BookRental.Domain.Interfaces.Services;
using BookRental.Infrastructure.Data;
using BookRental.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;

namespace Application;

public static class ApplicationServiceBuilder
{
    private static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookRentalDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .UseLazyLoadingProxies()
        );

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
        })
        .AddEntityFrameworkStores<BookRentalDbContext>()
        .AddDefaultTokenProviders();

        

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IDestinationRepository, DestinationRespository>();
        services.AddScoped<IRentRepository, RentRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddHttpContextAccessor();
        
        var jwtSettingsSection = configuration.GetSection(nameof(JwtSettings));
        var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
        services.Configure<JwtSettings>(jwtSettingsSection);

        var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            RequireSignedTokens = true
        };
        services.AddSingleton(tokenValidationParameters);
        services.AddScoped<ITokenGenerationService, TokenGenerationService>();
        
        services.AddLocalization();
        services.AddSingleton(sp =>
            sp.GetRequiredService<IStringLocalizerFactory>()
                .Create("BookRental.Resources.Resources", "BookRental.Resources"));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidation(fv =>
        {
            fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            fv.ImplicitlyValidateChildProperties = true;
        });

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
{
    services.AddCommonServices(configuration);
    var tokenValidationParameters = services.BuildServiceProvider()
        .GetRequiredService<TokenValidationParameters>();
    
    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = tokenValidationParameters;
    });

    return services;
}

    public static IServiceCollection AddWebAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCommonServices(configuration);
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Auth/Login";
            options.LogoutPath = "/Auth/Logout";
            options.AccessDeniedPath = "/Auth/Login";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.SlidingExpiration = true;
        });
        return services;
    }

    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roleNames = { Constants.RoleAdmin, Constants.RoleCustomer };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}