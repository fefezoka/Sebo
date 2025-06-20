﻿using System.ComponentModel.DataAnnotations;

namespace SEBO.Domain.Dto.DTO.IdentityDTO.Account
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(20, ErrorMessage = "Max length 20")]
        [MinLength(3, ErrorMessage = "Min length 2")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Compare("Password")]
        public string RePassword { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "Max length 50")]
        [MinLength(3, ErrorMessage = "Min length 3")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "Max length 50")]
        [MinLength(3, ErrorMessage = "Min length 3")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress(ErrorMessage = "The field {0} is invalid")]
        public string Email { get; set; }
    }
}