﻿using System.ComponentModel.DataAnnotations;

namespace infoManagerAPI.DTO.Authenticate.Request
{
    public class AuthenticationRequest
    {
        [Required]
        public string? UserEmail { get; set; }

        [Required]
        public string? Password { get; set; }

        public bool IsValid => Validate();

        private bool Validate()
        {
            return !string.IsNullOrEmpty(UserEmail) && !string.IsNullOrEmpty(Password);
        }
    }
}
