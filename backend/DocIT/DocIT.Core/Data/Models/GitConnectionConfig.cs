using System;
namespace DocIT.Core.Data.Models
{
    public class GitConnectionConfig
    {
        public Guid Id { get; set; }
        public string AccountName { get; set; }
        public string PersonalToken { get; set; }
        public string Type { get; set; } = "Github";
    }
}
