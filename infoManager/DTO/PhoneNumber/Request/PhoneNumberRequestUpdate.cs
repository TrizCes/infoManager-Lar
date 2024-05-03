﻿using infoManager.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace infoManagerAPI.DTO.PhoneNumber.Request
{
    public class PhoneNumberRequestUpdate
    {
        [Required(ErrorMessage = "Number is required")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [Range(0, 3, ErrorMessage = "Mobile = 0,\r\n        Residential = 1,\r\n        Commercial = 2,\r\n        EmergencyContact = 3")]
        public PhoneType Type { get; set; }
    }
}