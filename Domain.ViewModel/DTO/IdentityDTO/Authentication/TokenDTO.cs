using SEBO.API.Domain.Entities.IdentityAggregate;

namespace SEBO.API.Domain.ViewModel.DTO.IdentityDTO.Authentication
{
    public class TokenDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }

        public TokenDTO(ApplicationToken applicationToken)
        {
            AccessToken = applicationToken.AccessToken;
            RefreshToken = applicationToken.RefreshToken;
            RefreshTokenExpiration = applicationToken.RefreshTokenExpiration;
            AccessTokenExpiration = applicationToken.AccessTokenExpiration;
        }
    }
}