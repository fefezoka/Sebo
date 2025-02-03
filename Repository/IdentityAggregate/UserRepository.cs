using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SEBO.API.Data;
using SEBO.API.Domain.Entities.IdentityAggregate;
using System.Security.Claims;

namespace SEBO.API.Repository.IdentityAggregate
{
    public class UserRepository
    {
        private readonly SEBOContext _context;
        protected readonly DbSet<ApplicationUser> _dbSet;
        protected readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(SEBOContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _dbSet = _context.Set<ApplicationUser>();
            _userManager = userManager;

        }

        public async Task<ApplicationUser?> GetUserByIdAsync(int id) => await _userManager.FindByIdAsync(id.ToString());
        public async Task<ApplicationUser?> GetUserByUserNameAsync(string userName) => await _userManager.FindByNameAsync(userName);
        public async Task<ApplicationUser?> GetUserByEmailAsync(string email) =>  await _userManager.FindByEmailAsync(email);
        public async Task<IEnumerable<ApplicationUser>> GetActiveUsersAsync() => await _dbSet.Where(x => x.Active).ToListAsync();
        public async Task<IEnumerable<ApplicationUser>> GetAllUsersByUsernameAsync(string username) => await _dbSet.Where(user => user.UserName.Contains(username)).ToListAsync();
        public async Task<IEnumerable<ApplicationUser>?> GetAllUsersAsync() => await _dbSet.ToListAsync();
        public async Task<IEnumerable<ApplicationUser>> GetAllUsersByClaimAsync(Claim claim) => await _userManager.GetUsersForClaimAsync(claim);
        public async Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string roleName) => await _userManager.GetUsersInRoleAsync(roleName);

        public async Task<(IdentityResult, ApplicationUser)> AddUserAsync(ApplicationUser user, string password)
        {
            user.CreateDate = DateTime.Now;
            user.AlterDate = DateTime.Now;
            user.Type = "Standard";
            var createResult = await _userManager.CreateAsync(user, password);
            var userResult = await _userManager.FindByNameAsync(user.UserName);
            return (createResult, userResult);
        }

        public async Task<(IdentityResult, ApplicationUser)> UpdateUserAsync(int id, ApplicationUser user)
        {

            var userEntity = await _userManager.FindByIdAsync(id.ToString());
            if (userEntity is null) return (IdentityResult.Failed(), null);

            if (user.FirstName is not null) userEntity.FirstName = user.FirstName;
            if (user.LastName is not null) userEntity.LastName = user.LastName;

            if (user.UserName is not null)
            {
                userEntity.UserName = user.UserName;
                userEntity.NormalizedUserName = user.UserName.ToUpper();
            }

            if (user.PhoneNumber is not null)
            {
                userEntity.PhoneNumber = user.PhoneNumber;
                userEntity.PhoneNumberConfirmed = false;
            }

            if ((user.Email != null) && (userEntity.Email.ToUpper() != user.Email.ToUpper()))
            {
                userEntity.Email = user.Email;
                userEntity.NormalizedEmail = user.Email.ToUpper();
                userEntity.EmailConfirmed = false;
            }

            userEntity.AlterDate = DateTime.Now;
            var updateResult = await _userManager.UpdateAsync(userEntity);
            return (updateResult, userEntity);
        }

        public async Task<IdentityResult> DeleteUserByIdAsync(int id)
        {
            var userDatabase = await _userManager.FindByIdAsync(id.ToString());
            return userDatabase != null ? await DeleteUserAsync(userDatabase) : IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteUserByUserNameAsync(string username)
        {
            var userDatabase = await _userManager.FindByNameAsync(username);
            return userDatabase != null ? await DeleteUserAsync(userDatabase) : IdentityResult.Failed();
        }
        private async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            if (user != null)
            {
                user.AlterDate = DateTime.Now;
                user.Active = false;
                await _userManager.UpdateAsync(user);
                return await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            }
            return IdentityResult.Failed();
        }
    }
}