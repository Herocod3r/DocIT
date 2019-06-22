using System;
using System.Collections.Generic;
using DocIT.Core.Data.Models;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using DocIT.Core.Repositories;
using MongoDB.Bson;
using Microsoft.AspNetCore.Identity;
using DocIT.Service.Models;
using DocIT.Core.Services.Exceptions;

namespace DocIT.Service.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public User CreateNew(User item)
        {
            throw new InvalidOperationException("A user must be associated with a password");
        }

        public User CreateUserAccount(User user, string password)
        {
            var appUser = new Models.ApplicationUser { UserName = user.Email, DateJoined = DateTime.Now, Email = user.Email, UserAccount = user};
            var res = userManager.CreateAsync(appUser, password).Result;
            if (!res.Succeeded) throw new ArgumentException(res.Errors.FirstOrDefault().Description);

            appUser.UserAccount.Id = appUser.Id;
            userManager.UpdateAsync(appUser).Wait();

            return appUser.UserAccount;
        }

        public void Delete(User item)
        {
            var user = userManager.FindByIdAsync(item.Id.ToString()).Result;
            if (user is null) throw new ArgumentException("User not found");
            userManager.DeleteAsync(user).Wait();
        }

        public User FindUserByEmailAndPassword(string email, string password)
        {
            var user = userManager.FindByEmailAsync(email).Result;
            if (user is null) throw new ArgumentException("Email Or Password Is Incorrect");
            var result =  signInManager.CheckPasswordSignInAsync(user, password, false).Result;
            if (!result.Succeeded) throw new ArgumentException("Email Or Password Is Incorrect");
            return user.UserAccount;
        }

        public User GetById(Guid id) => QueryAsync().Where(x => x.Id == id).FirstOrDefault();


        public IQueryable<User> QueryAsync() => userManager.Users.Select(x => x.UserAccount);

        public void Update(User item)
        {
            var user = userManager.FindByIdAsync(item.Id.ToString()).Result;
            if (user is null) throw new ArgumentException("User cannot be found");
            user.UserAccount = item;
            userManager.UpdateAsync(user).Wait();
        }
    }
}
