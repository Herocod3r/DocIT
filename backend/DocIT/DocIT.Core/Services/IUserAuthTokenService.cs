using System;
namespace DocIT.Core.Services
{
    public interface IUserAuthTokenService
    {
        string GenerateToken(params (string,object)[] bodyItems);
    }
}
