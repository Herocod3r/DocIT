using System;
using System.Threading.Tasks;
using DocIT.Service.Models;

namespace DocIT.Service.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> LoginAsync(LoginPayload payload);
        Task RegisterAsync(RegisterPayload payload);
    }
}
