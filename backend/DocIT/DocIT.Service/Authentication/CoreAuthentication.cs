using System;
using System.Linq;
using System.Threading.Tasks;
using DocIT.Core.Repositories;
using DocIT.Service.Models;
using Microsoft.AspNetCore.Identity;

namespace DocIT.Service.Authentication
{
    public class CoreAuthentication : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUserRepository userRepository;
        private readonly IUserAuthTokenService tokenizer;

        public CoreAuthentication(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserRepository userRepository, IUserAuthTokenService tokenizer)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userRepository = userRepository;
            this.tokenizer = tokenizer;
        }

        public async Task<string> LoginAsync(LoginPayload login)
        {
            var user = await userManager.FindByEmailAsync(login.Email);
            if (user is null) throw new AuthException("Email Or Password Is Incorrect");
            var result = await signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (result.Succeeded) return tokenizer.GenerateToken( ("ID",user.Id), ("Email",user.Email));

            throw new AuthException("Email Or Password Is Incorrect");
        }

        public async Task RegisterAsync(RegisterPayload payload)
        {
            var appUser = new Models.ApplicationUser { UserName = payload.Email, Email = payload.Email};
            var res = await userManager.CreateAsync(appUser, payload.Password);
            if (!res.Succeeded) throw new AuthException(res.Errors.FirstOrDefault().Description);
            var user = userRepository.CreateNew(new Core.Data.Models.User { DateCreated = DateTime.Now, Email = payload.Email, Name = payload.Name });
            appUser.UserID = user.Id;
            await userManager.UpdateAsync(appUser);

        }
    }
}
