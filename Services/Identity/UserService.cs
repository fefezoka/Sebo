using System.Security.Claims;
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
        public UserService(UserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
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

            return new ReadUserDTO(user);
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

            var (result, user) = await _userRepository.UpdateUserAsync(userId, applicationUser);

            return new ReadUserDTO(user);
        }

        public async Task<IEnumerable<ReadUserDTO>> FindAll() => (await _userRepository.GetAllUsersAsync()).Select(x => new ReadUserDTO(x));
        
        public async Task<ReadUserDTO> GetUser()
        {
            var email = GetUserEmailFromClaims();
            var user = await _userRepository.GetUserByEmailAsync(email) ?? throw new NotFoundException("User not found.");

            return new ReadUserDTO(user);
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
