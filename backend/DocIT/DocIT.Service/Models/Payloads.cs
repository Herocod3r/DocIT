using System;
using System.ComponentModel.DataAnnotations;
using DocIT.Core.Data.Payloads;

namespace DocIT.Service.Models
{
    public class RegisterPayload : UserAccountPayload
    {
       
        [Required]
        public string Password { get; set; }
         
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
    }


    public class LoginPayload
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
