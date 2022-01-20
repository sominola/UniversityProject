using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UniversityProject.Data.Entities;
using UniversityProject.Data.Repositories.Interfaces;
using UniversityProject.Domain.Services.Interfaces;

namespace UniversityProject.Domain.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public TokenService(IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _passwordHasher = new PasswordHasher<User>();
    }


    public string GenerateJwt(IEnumerable<Claim> claims)
    {
        var symmetricSecurityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.Key));
        var credentials =
            new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            AuthOptions.Issuer,
            AuthOptions.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(120),
            signingCredentials: credentials);
        var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return encodedToken;
    }
}