using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SEBO.API.Domain.Entities.IdentityAggregate;
using SEBO.API.Domain.Utility.Exceptions;
using SEBO.API.Domain.ViewModel.DTO.IdentityDTO;
using SEBO.API.Repository.IdentityAggregate;
using SEBO.Domain.ViewModel.DTO.IdentityDTO;

namespace SEBO.API.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(UserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IdentityResult> RegisterAccountAsync(CreateUserDTO createUserDto)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = createUserDto.UserName,
                LastName = createUserDto.LastName,
                Email = createUserDto.Email,
                FirstName = createUserDto.FirstName,
            };

            var (result, user) = await _userRepository.AddUserAsync(applicationUser, createUserDto.Password);

            return result;
        }

        public async Task<IdentityResult> UpdateUser(UpdateUserDto updateUserDto)
        {
            var userId = GetUserIdFromClaims();

            var applicationUser = new ApplicationUser()
            {
                UserName = updateUserDto.UserName,
                LastName = updateUserDto.LastName,
                Email = updateUserDto.Email,
                FirstName = updateUserDto.FirstName,
            };

            var (result, newUser) = await _userRepository.UpdateUserAsync(userId, applicationUser);

            return result;
        }

        public string GetUserEmailFromClaims()
        {
            return _httpContextAccessor.HttpContext?.User?.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
        }

        public int GetUserIdFromClaims()
        {
            return int.Parse(_httpContextAccessor.HttpContext?.User?.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);
        }

        public async Task<ApplicationUser> GetUser()
        {
            var email = GetUserEmailFromClaims();
            return await _userRepository.GetUserByEmailAsync(email) ?? throw new NotFoundException("User not found.");
        }
    }
}
