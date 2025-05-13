using System.ComponentModel.DataAnnotations;

namespace SEBO.API.Domain.ViewModel.DTO.IdentityDTO.Account
{
    public class UpdateUserDTO
    {
        [MaxLength(20, ErrorMessage = "Max length 20")]
        [MinLength(3, ErrorMessage = "Min length 2")]
        public string? UserName { get; set; }

        [MaxLength(50, ErrorMessage = "Max length 50")]
        [MinLength(3, ErrorMessage = "Min length 3")]
        public string? FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Max length 50")]
        [MinLength(3, ErrorMessage = "Min length 3")]
        public string? LastName { get; set; }

        public string? Email { get; set; }
    }
}