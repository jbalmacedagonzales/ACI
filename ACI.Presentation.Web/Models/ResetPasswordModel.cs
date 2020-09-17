using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ACI.Presentation.Web.Models
{
    public class ResetPasswordModel
    {

        [HiddenInput]
        public string Token { get; set; }

        [HiddenInput]
        public string Email { get; set; }


        public string NewPassword { get; set; }

        public string ConfirmNewPassword { get; set; }


        [Required]
        [HiddenInput]
        public string NewPasswordHash { get; set; }

        [Required]
        [HiddenInput]
        [Compare("NewPasswordHash")]
        public string ConfirmNewPasswordHash { get; set; }


    }
}
