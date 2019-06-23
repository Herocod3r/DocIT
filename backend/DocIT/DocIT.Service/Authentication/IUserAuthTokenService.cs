using System;
namespace DocIT.Service.Authentication
{
    public interface IUserAuthTokenService
    {
        string GenerateToken(params (string,object)[] bodyItems);
    }
}
