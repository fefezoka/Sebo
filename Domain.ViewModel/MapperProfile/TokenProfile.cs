using AutoMapper;
using SEBO.API.Domain.Entities.IdentityAggregate;
using SEBO.API.Domain.ViewModel.DTO.IdentityDTO;

namespace SEBO.API.Domain.ViewModel.MapperProfile
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            CreateMap<ApplicationToken, TokenDTO>();
        }
    }
}