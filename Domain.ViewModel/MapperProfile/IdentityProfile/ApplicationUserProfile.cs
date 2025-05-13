using AutoMapper;
using SEBO.API.Domain.Entities.IdentityAggregate;
using SEBO.API.Domain.ViewModel.DTO.IdentityDTO.Account;

namespace SEBO.API.Domain.ViewModel.MapperProfile.IdentityProfile
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, ReadUserDTO>();
        }
    }
}