using System;
using System.Threading.Tasks;
using DocIT.Core.Data.Models;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Data.ViewModels;
using DocIT.Core.Repositories;
using System.Linq;

namespace DocIT.Core.Services.Implementations
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<User> GetUserByIdAsync(Guid id) => await Task.Run(()=> repository.GetById(id));

        public async Task<ListViewModel<User>> ListUsersAsync(int skip, int limit = 30)
        {
            return new ListViewModel<User>
            {
                Result =  repository.QueryAsync().OrderByDescending(x => x.DateCreated).Skip(skip).Take(limit).ToList(),
                Total = repository.QueryAsync().Count()
            };
        }

        public async Task UpdateUserAsync(UpdateAccountPayload payload, Guid userId)
        {
            var user = repository.GetById(userId);
            if (user is null) throw new ArgumentException("User not found");
            user.Name = payload.Name;
            repository.Update(user);
        }
    }
}
