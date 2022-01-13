using System.Security.Claims;
using UniversityProject.Data.Entities;
using UniversityProject.Domain.Dto;
using UniversityProject.Domain.Dto.User;

namespace UniversityProject.Domain.Services.Interfaces;

public interface ITokenService
{
    string GenerateJwt(IEnumerable<Claim> claims);
}