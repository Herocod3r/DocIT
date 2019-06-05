using System;
namespace DocIT.Core.Data.Models
{
    public class GitConnectionConfig : DbModel<Guid>
    {
        public string AccountName { get; set; }
        public string PersonalToken { get; set; }
        public string Type { get; set; } = "Github";
    }
}
