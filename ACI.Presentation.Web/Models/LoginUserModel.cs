using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ACI.Presentation.Web.Models
{
    public class LoginUserModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }

        public bool RememberPassword { get; set; }


        [Required]
        [HiddenInput]
        public string PasswordHash { get; set; }
    }
}
