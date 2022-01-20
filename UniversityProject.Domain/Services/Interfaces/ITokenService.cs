using System.Security.Claims;

namespace UniversityProject.Domain.Services.Interfaces;

public interface ITokenService
{
    string GenerateJwt(IEnumerable<Claim> claims);
}