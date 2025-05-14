using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SEBO.API.Domain.Entities.IdentityAggregate;
using SEBO.API.Domain.Utility.Exceptions;
using SEBO.API.Domain.ViewModel.DTO.IdentityDTO.Account;
using SEBO.API.Repository.IdentityAggregate;

namespace SEBO.API.Services.Identity
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public UserService(UserRepository userRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ReadUserDTO> RegisterAccountAsync(CreateUserDTO createUserDto)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = createUserDto.UserName,
                LastName = createUserDto.LastName,
                Email = createUserDto.Email,
                FirstName = createUserDto.FirstName,
            };

            var (result, user) = await _userRepository.AddUserAsync(applicationUser, createUserDto.Password);

            return _mapper.Map<ReadUserDTO>(user);
        }

        public async Task<ReadUserDTO> UpdateUser(UpdateUserDTO updateUserDTO)
        {
            var userId = GetUserIdFromClaims();

            var applicationUser = new ApplicationUser()
            {
                UserName = updateUserDTO.UserName,
                LastName = updateUserDTO.LastName,
                Email = updateUserDTO.Email,
                FirstName = updateUserDTO.FirstName,
            };

            var (result, newUser) = await _userRepository.UpdateUserAsync(userId, applicationUser);

            return _mapper.Map<ReadUserDTO>(newUser);
        }

        public async Task<IEnumerable<ReadUserDTO>> FindAll() => _mapper.Map<IEnumerable<ReadUserDTO>>(await _userRepository.GetAllUsersAsync());
        
        public async Task<ReadUserDTO> GetUser()
        {
            var email = GetUserEmailFromClaims();
            return _mapper.Map<ReadUserDTO>(await _userRepository.GetUserByEmailAsync(email) ?? throw new NotFoundException("User not found."));
        }

        private string GetUserEmailFromClaims()
        {
            return _httpContextAccessor.HttpContext?.User?.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
        }

        private int GetUserIdFromClaims()
        {
            return int.Parse(_httpContextAccessor.HttpContext?.User?.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);
        }

       
    }
}
