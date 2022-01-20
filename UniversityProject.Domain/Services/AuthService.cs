using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using UniversityProject.Data.Entities;
using UniversityProject.Data.Repositories.Interfaces;
using UniversityProject.Domain.Dto.User;
using UniversityProject.Domain.Exceptions;
using UniversityProject.Domain.Services.Interfaces;

namespace UniversityProject.Domain.Services;

public class AuthService: IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _context;

    public AuthService(ITokenService tokenService, IUnitOfWork unitOfWork, IHttpContextAccessor context)
    {
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _passwordHasher = new PasswordHasher<User>();
        _context = context;
    }

    public async Task Login(LoginDto model)
    {
        var result = await VerifyUserCredentials(model);
        var claims = GenerateClaims(result);

        var jwt = _tokenService.GenerateJwt(claims);
       _context.HttpContext.Response.Cookies.Append("Token", jwt);
    }

    public async Task Login(string email, string password)
    {
         await Login(new LoginDto
        {
            Email = email,
            Password = password
        });
    }

    public Task Logout()
    {
        _context.HttpContext.Response.Cookies.Delete("Token");
        return Task.CompletedTask;
    }

    private IEnumerable<Claim> GenerateClaims(User user)
    {
        var claims = new List<Claim>()
        {
            new("id", user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
        };
        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
        }

        return claims;
    }

    private async Task<User> VerifyUserCredentials(LoginDto model)
    {
        var user = await _unitOfWork.UserRepository.GetCredentialUserAsync(model.Email);

        if (user == null)
        {
            throw new PageResultException("User not found",HttpStatusCode.NotFound);
        }


        var isVerified = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, model.Password);

        if (isVerified == PasswordVerificationResult.Success)
        {
            return user;
        }
        else
        {
            throw new PageResultException("Password doesn't fit",HttpStatusCode.Forbidden);
        }
    }
}

