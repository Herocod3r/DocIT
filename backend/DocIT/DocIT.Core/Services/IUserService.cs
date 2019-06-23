using System;
using System.Threading.Tasks;
using DocIT.Core.Data.Models;
using System.Collections.Generic;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Data.ViewModels;

namespace DocIT.Core.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<ListViewModel<User>> ListUsersAsync(int skip, int limit = 30);
        Task UpdateUserAsync(UpdateAccountPayload user, Guid userId);

    }
}
