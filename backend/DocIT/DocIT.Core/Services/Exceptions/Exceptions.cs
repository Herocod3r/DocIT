using System;
namespace DocIT.Core.Services.Exceptions
{


    public class GitConfigException : Exception
    {
        public GitConfigException(string message) : base(message)
        {

        }
    }
}
