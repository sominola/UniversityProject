using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UniversityProject.Data.Context;
using UniversityProject.Data.Repositories;
using UniversityProject.Data.Repositories.Interfaces;
using UniversityProject.Domain;
using UniversityProject.Domain.Dto.User;
using UniversityProject.Domain.Mapping;
using UniversityProject.Domain.Services.Implementation;
using UniversityProject.Domain.Services.Interfaces;

namespace UniversityProject.Web.Extensions;

public static class ConfigureExtension
{
    private static IWebHostEnvironment Environment { get; set; }
    private static IConfiguration Configuration { get; set; }

    public static void InitConfigure(this WebApplicationBuilder builder)
    {
        Environment = builder.Environment;
        Configuration = builder.Configuration;
    }

    public static void AddMiddlewares(this IApplicationBuilder app)
    {
        if (Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.Use(async (context, next) =>
        {
            var contains = context.Request.Cookies.TryGetValue("Token", out var token); 
            if (contains && !string.IsNullOrEmpty(token))  
            {
                context.Request.Headers.Add("Authorization", "Bearer " + token);
            }
            await next();
        });
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddDbContext();
        services.AddMapper();
        services.AddJwtAuthentication();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<AuthService>();
        services.AddValidation();
        services.AddRazorPages().AddRazorRuntimeCompilation();
        services.AddHttpContextAccessor();
    }

    private static void AddDbContext(this IServiceCollection services)
    {
        string connectionString;

        if (Environment.IsDevelopment())
        {
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }
        else
        {
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        // services.AddDbContext<AppDbContext>(options =>
        //     options.UseSqlServer(connectionString, x => x.MigrationsAssembly("UniversityProject.Web")));
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));
    }

    private static void AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = AuthOptions.Issuer,
                    ValidAudience = AuthOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.Key)),
                };
                
            });
    }

    private static void AddMapper(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new UserMapper()); });
        var mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
    }

    private static void AddValidation(this IServiceCollection services)
    {
        services.AddFluentValidation(cfg =>
        {
            cfg.RegisterValidatorsFromAssemblyContaining<LoginDto>();
        });
    }
    
}