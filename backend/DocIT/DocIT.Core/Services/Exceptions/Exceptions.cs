using System;
namespace DocIT.Core.Services.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException(string message) : base(message)
        {

        }
    }
}
