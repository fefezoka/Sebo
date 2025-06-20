﻿using System.ComponentModel.DataAnnotations;

namespace SEBO.Domain.Dto.DTO.IdentityDTO.Authentication
{
    public class LoginRequestByUserNameDTO
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(20, ErrorMessage = "Max length 20")]
        [MinLength(3, ErrorMessage = "Min length 2")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}