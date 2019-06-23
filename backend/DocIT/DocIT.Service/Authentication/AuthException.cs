using System;
namespace DocIT.Service.Authentication
{
    public class AuthException : Exception
    {
        public AuthException(string message) : base(message)
        {

        }
    }

}
