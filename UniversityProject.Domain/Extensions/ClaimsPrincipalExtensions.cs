using System.Security.Claims;
using UniversityProject.Data.Entities;
using UniversityProject.Data.Repositories.Interfaces;

namespace UniversityProject.Domain.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static long GetCurrentUserId(this ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }

        var claim = principal.Claims.First(c => c.Type == "id");
        return long.Parse(claim.Value);
    }
}