using System.Security.Claims;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using SEBO.Domain.Entities.IdentityAggregate;

namespace SEBO.Domain.Interface.Repository.IdentityAggregate
{
    public interface IUserRepository
    {
        Task<(Result result, ApplicationUser? user)> GetUserByIdAsync(int id);
        Task<(Result result, ApplicationUser? user)> GetUserByUserNameAsync(string userName);
        Task<(Result result, ApplicationUser? user)> GetUserByEmailAsync(string email);
        Task<IEnumerable<ApplicationUser>> GetActiveUsersAsync();
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<IEnumerable<ApplicationUser>> GetAllUsersByClaimAsync(Claim claim);
        Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string roleName);
        Task<IEnumerable<ApplicationUser>> GetAllUsersByUsernameAsync(string username);
        Task<(IdentityResult result, ApplicationUser user)> AddUserAsync(ApplicationUser user, string password);
        Task<(IdentityResult result, ApplicationUser user)> UpdateUserAsync(int id, ApplicationUser user);
        Task<IdentityResult> DeleteUserByIdAsync(int id);
        Task<IdentityResult> DeleteUserByUserNameAsync(string userName);
    }
}
