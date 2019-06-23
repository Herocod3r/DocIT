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

    public class ProjectViewModel
    {
        public Guid Id { get; set; }
        public int NoOfSubProjects { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SwaggerUrl { get; set; }
        public List<string> PreviewLinks { get; set; }
        public Guid? ParentId { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public class InviteViewModel
    {
        public string Email { get; set; }
        public string ProjectName { get; set; }
        public string ProjectId { get; set; }
        public string Inviter { get; set; }
    }
}
