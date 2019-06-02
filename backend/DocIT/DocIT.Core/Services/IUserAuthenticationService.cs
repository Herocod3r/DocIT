using System;
using System.Threading.Tasks;
using DocIT.Core.Data.Models;
using System.Collections.Generic;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Data.ViewModels;

namespace DocIT.Core.Services
{
    public interface IUserAuthenticationService
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<ListViewModel<User>> ListUsersAsync(int skip, int limit);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> RegisterUserAsync(RegisterPayload payload);
        Task UpdateUserAsync(User user);
        Task<LoginViewModel> LoginUserAsync(LoginPayload login);

    }
}
