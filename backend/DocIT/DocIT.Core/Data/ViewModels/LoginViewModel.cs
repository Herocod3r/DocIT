using System;
using DocIT.Core.Data.Models;

namespace DocIT.Core.Data.ViewModels
{
    public class LoginViewModel
    {
        public string Token { get; set; }
        public User User { get; set; }
    }
}
