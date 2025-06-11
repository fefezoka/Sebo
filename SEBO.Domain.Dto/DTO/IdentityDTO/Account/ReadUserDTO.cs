using SEBO.Domain.Entities.IdentityAggregate;

namespace SEBO.Domain.Dto.DTO.IdentityDTO.Account
{
    public class ReadUserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? AlterDate { get; set; }
        public bool Active { get; set; }

        public ReadUserDTO(ApplicationUser applicationUser)
        {
            Id = applicationUser.Id;
            FirstName = applicationUser.FirstName;
            LastName = applicationUser.LastName;
            UserName = applicationUser.UserName;
            Email = applicationUser.Email;
            PhoneNumber = applicationUser.PhoneNumber;
            LockoutEnd = applicationUser.LockoutEnd;
            CreateDate = applicationUser.CreateDate;
            CreateDate = applicationUser.CreateDate;
            AlterDate = applicationUser.AlterDate;
            Active = applicationUser.Active;
        }
    }
}