using System.Security.Claims;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using SEBO.Domain.Entities.IdentityAggregate;

namespace SEBO.Domain.Interface.Services.Identity
{
    public interface ITokenService
    {
        Task<Result<ApplicationToken>> GetToken(IdentityUser<int> user, IList<Claim> claims, IList<string> roles);
    }
}