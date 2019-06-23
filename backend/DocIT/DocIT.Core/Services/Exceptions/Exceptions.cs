using System;
namespace DocIT.Core.Services.Exceptions
{


    public class GitConfigException : Exception
    {
        public GitConfigException(string message) : base(message)
        {

        }
    }

    public class GitResolverException : Exception
    {
        public GitResolverException(string message) : base(message)
        {

        }

    }
}
