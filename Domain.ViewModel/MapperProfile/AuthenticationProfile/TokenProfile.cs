using AutoMapper;
using SEBO.API.Domain.Entities.IdentityAggregate;
using SEBO.API.Domain.ViewModel.DTO.IdentityDTO.Authentication;

namespace SEBO.API.Domain.ViewModel.MapperProfile.AuthenticationProfile
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            CreateMap<ApplicationToken, TokenDTO>();
        }
    }
}