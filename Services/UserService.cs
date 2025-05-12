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
            var applicationUser = new ApplicationUser()
            {
                UserName = updateUserDto.UserName,
                LastName = updateUserDto.LastName,
                Email = updateUserDto.Email,
                FirstName = updateUserDto.FirstName,
            };

            //TODO: ARRUMAR ID QUANDO IMPLEMENTAR JWT
            var (result, user) = await _userRepository.UpdateUserAsync(1, applicationUser);

            return result;
        }

        public async Task<ApplicationUser> GetUser()
        {
            var email = _httpContextAccessor.HttpContext?.User?.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            return await _userRepository.GetUserByEmailAsync(email) ?? throw new NotFoundException("User not found.");
        }
    }
}
