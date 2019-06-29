using System;
namespace DocIT.Core.Data.Models
{
    public class GitResolverItem
    {
        public GitConnectionConfig GitConnection { get; set; }
        public string RepoName { get; set; }
        public string FilePath { get; set; }
        public string Branch { get; set; }
    }
}
