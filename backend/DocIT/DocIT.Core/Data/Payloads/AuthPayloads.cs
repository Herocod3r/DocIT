using System;
using System.ComponentModel.DataAnnotations;

namespace DocIT.Core.Data.Payloads
{
    public class RegisterPayload
    {
        [Required]
        public string Name { get; set; }

        [Required,EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required,Compare("Password")]
        public string ConfirmPassword { get; set; }
    }


    public class LoginPayload
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class UpdateAccountPayload
    {
        [Required]
        public string Name { get; set; }

    }
}
