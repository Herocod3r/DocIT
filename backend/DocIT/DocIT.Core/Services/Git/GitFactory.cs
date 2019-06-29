using System;
using DocIT.Core.Data.Models;
namespace DocIT.Core.Services.Git
{
    public static class GitFactory
    {
        public static IGitResolver GetResolver(string gitType)
        {
            switch (gitType)
            {
                case "Github":
                    return new GithubResolver();
                default:
                    throw new ArgumentException("Failed to find resolver for this item");
            }
        }
    }
}
