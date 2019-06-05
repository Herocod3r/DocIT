using System;
using System.Collections.Generic;
using DocIT.Core.Data.Models;

namespace DocIT.Core.Data.ViewModels
{
    public class ListViewModel<T>
    {
        public List<T> Result { get; set; }
        public Int64 Total { get; set; }
    }

    public class LoginViewModel
    {
        public string Token { get; set; }
        public User User { get; set; }
    }


    public class GitConfigViewModel
    {
        public Guid Id { get; set; }
        public string AccountName { get; set; }
        public string PersonalToken { get; set; }
        public string Type { get; set; } = "Github";
    }
}
