using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using UniversityProject.Data.Entities;
using UniversityProject.Data.Models;
using UniversityProject.Data.Repositories.Interfaces;
using UniversityProject.Domain.Dto.User;
using UniversityProject.Domain.Services.Interfaces;

namespace UniversityProject.Domain.Services.Implementation;

public class AuthService
{
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _context;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(ITokenService tokenService, IHttpContextAccessor context, IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _context = context;
        _unitOfWork = unitOfWork;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<Result<string>> Login(LoginDto model)
    {
        var result = await VerifyUserCredentials(model);
        if (result.Error == null)
        {
            var user = result.Data;
            var claims = new List<Claim>()
            {
                new("id", user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
            };
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var jwt = _tokenService.GenerateJwt(claims);
            return Result<string>.GetSuccess(jwt);
        }

        return new Result<string>
        {
            Error = result.Error
        };

    }

    private async Task<Result<User>> VerifyUserCredentials(LoginDto model)
    {
        var user = await _unitOfWork.UserRepository.GetCredentialUserAsync(model.Email);

        if (user == null)
        {
            return Result<User>.GetError(ErrorCode.NotFound, "User not found");
        }


        var isVerified = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, model.Password);

        if (isVerified == PasswordVerificationResult.Success)
        {
            return Result<User>.GetSuccess(user);
        }

        return Result<User>.GetError(ErrorCode.Forbidden, "Password doesn't fit");
    }
}