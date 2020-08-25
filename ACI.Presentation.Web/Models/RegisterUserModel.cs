using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace ACI.Presentation.Web.Models
{
    public class RegisterUserModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

       
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }


        [Required]
        [HiddenInput]
        public string PasswordHash { get; set; }

        [Required]
        [HiddenInput]
        [Compare("PasswordHash")]
        public string ConfirmPasswordHash { get; set; }
    }
}
