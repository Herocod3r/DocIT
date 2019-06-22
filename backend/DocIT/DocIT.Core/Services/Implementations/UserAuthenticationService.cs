using System;
using System.Threading.Tasks;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Data.Models;
using DocIT.Core.Data.ViewModels;
using DocIT.Core.Repositories;
using DocIT.Core.Services.Exceptions;
using System.Linq;

namespace DocIT.Core.Services.Implementations
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserRepository repository;
        private readonly IUserAuthTokenService userAuthToken;

        public UserAuthenticationService(IUserRepository repository,IUserAuthTokenService userAuthToken)
        {
            this.repository = repository;
            this.userAuthToken = userAuthToken;
        }

        public async Task<User> GetUserByEmailAsync(string email) => await Task.Run(() => repository.QueryAsync().Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault());

        public async Task<User> GetUserByIdAsync(Guid id) => await Task.Run(() => repository.GetById(id));

        public async Task<ListViewModel<User>> ListUsersAsync(int skip, int limit)
        {
            return new ListViewModel<User>
            {
                Result = this.repository.QueryAsync().OrderByDescending(x => x.DateCreated).Skip(skip).Take(limit).ToList(),
                Total = this.repository.QueryAsync().Count()
            };
        }

        public async Task<LoginViewModel> LoginUserAsync(LoginPayload login)
        {
            try
            {
                var user = await Task.Run(() => repository.FindUserByEmailAndPassword(login.Email, login.Password));
                return new LoginViewModel { Token = userAuthToken.GenerateToken(user.Id, user.Email), User = user };
            }
            catch (ArgumentException ex)
            {
                throw new AuthException(ex.Message);
            }
        }

        public async Task<User> RegisterUserAsync(RegisterPayload payload)
        {
            try
            {
               return await Task.Run(()=> repository.CreateUserAccount(new User { DateCreated = DateTime.Now, Email = payload.Email, Name = payload.Name }, payload.Password));
               
            }
            catch (ArgumentException ex)
            {
                throw new AuthException(ex.Message);
            }
        }

        public async Task UpdateUserAsync(UpdateAccountPayload user, Guid userId)
        {
            try
            {
                var rsp = repository.QueryAsync().FirstOrDefault(x => x.Id == userId);
                if (rsp is null) throw new AuthException("User not found");
                rsp.Name = user.Name;
                await Task.Run(() => repository.Update(rsp));
            }
            catch (ArgumentException ex)
            {
                throw new AuthException(ex.Message);
            }
        }
    }
}
