using System.Text;
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
using UniversityProject.Domain.Services;
using UniversityProject.Domain.Services.Interfaces;
using UniversityProject.Web.Filters;
using UniversityProject.Web.Middlewares;

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
        app.UseJwtTokens();
        app.UseStatusCodePagesWithReExecute("/error", "?code={0}");
        app.UseHttpsRedirection();


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
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ILessonService, LessonService>();
        services.AddScoped<ILessonService, LessonService>();
        services.AddValidation();
        services.AddRazorPages().AddMvcOptions(options =>
        {
            options.Filters.Add(new ExceptionHandlerFilter());
            options.Filters.Add(new ModelValidationFilter());
        }).AddRazorRuntimeCompilation();
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
        services.AddHttpContextAccessor();
        services.AddCors();
    }

    private static void UseJwtTokens(this IApplicationBuilder builder)
    { 
        builder.UseMiddleware<JwtTokenMiddleware>();
    }
    private static void AddDbContext(this IServiceCollection services)
    {
        var connectionString = Configuration.GetConnectionString("DefaultConnection");

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
                    ValidateIssuerSigningKey =
                        true,
                    ValidIssuer = AuthOptions.Issuer,
                    ValidAudience = AuthOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.Key)),
                };
            });
    }

    private static void AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserMapper));
    }

    private static void AddValidation(this IServiceCollection services)
    {
        services.AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<LoginDto>(); });
    }
    
  
}