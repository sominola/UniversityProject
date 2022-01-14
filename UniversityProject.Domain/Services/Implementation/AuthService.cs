using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using UniversityProject.Data.Entities;
using UniversityProject.Data.Repositories.Interfaces;
using UniversityProject.Domain.Dto.User;
using UniversityProject.Domain.Models;
using UniversityProject.Domain.Services.Interfaces;

namespace UniversityProject.Domain.Services.Implementation;

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

    public async Task<IResult> Login(LoginDto model)
    {
        var result = await VerifyUserCredentials(model);

        if (!result.IsSuccess)
            return result;

        var claims = GenerateClaims(result.Data);

        var jwt = _tokenService.GenerateJwt(claims);
       _context.HttpContext.Response.Cookies.Append("Token", jwt);
       return new Result().Success();
    }

    public async Task<IResult> Login(string email, string password)
    {
        return await Login(new LoginDto
        {
            Email = email,
            Password = password
        });
    }

    public Task<IResult> Logout()
    {
        _context.HttpContext.Response.Cookies.Delete("Token");
        return Task.Run(()=>new Result().Success());
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

    private async Task<IResult<User>> VerifyUserCredentials(LoginDto model)
    {
        var user = await _unitOfWork.UserRepository.GetCredentialUserAsync(model.Email);

        if (user == null)
        {
            return new Result<User>().SetError(ErrorCode.NotFound, "User not found");
        }


        var isVerified = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, model.Password);

        if (isVerified == PasswordVerificationResult.Success)
        {
            return new Result<User>().Success(user);
        }
        else
        {
            return new Result<User>().SetError(ErrorCode.Forbidden, "Password doesn't fit");
        }
    }
}

