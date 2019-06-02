using System;
using System.Threading.Tasks;
using DocIT.Core.Data.Models;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Data.ViewModels;
using DocIT.Core.Services;
using DocIT.Service.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using DocIT.Core.Services.Exceptions;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace DocIT.Service
{
    public class AuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly Settings settings;

        public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,Models.Settings settings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.settings = settings;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
           var item = await userManager.FindByEmailAsync(email);
            return item?.UserAccount;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var item = await userManager.FindByIdAsync(id.ToString());
            return item?.UserAccount;
        }

        public async Task<ListViewModel<User>> ListUsersAsync(int skip, int limit)
        {
            return new ListViewModel<User>
            {
                Result = userManager.Users.OrderByDescending(x=>x.DateJoined).Skip(skip).Take(limit).Select(x=>x.UserAccount).ToList(),
                Total = userManager.Users.Count()
            };
        }

        public async Task<LoginViewModel> LoginUserAsync(LoginPayload login)
        {
            var user = await userManager.FindByEmailAsync(login.Email);
            if (user is null) throw new AuthException("Email Or Password Is Incorrect");
            var result = await signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (result.Succeeded ) return new LoginViewModel { Token = GenerateToken(user.Id.ToString()), User = user.UserAccount };

             throw new AuthException("Email Or Password Is Incorrect");
        }

        public async Task<User> RegisterUserAsync(RegisterPayload payload)
        {
            var appUser = new Models.ApplicationUser { UserName = payload.Email, DateJoined = DateTime.Now, Email = payload.Email, UserAccount = new User { Email = payload.Email, Name = payload.Name } };
            var res = await userManager.CreateAsync(appUser, payload.Password);
            if (!res.Succeeded) throw new AuthException(res.Errors.FirstOrDefault().Description);

            appUser.UserAccount.Id = appUser.Id;
            await userManager.UpdateAsync(appUser);

            return appUser.UserAccount;

        }

        public async Task UpdateUserAsync(User user)
        {
            var appUser = await userManager.FindByIdAsync(user.Id.ToString());
            if (appUser is null) throw new AuthException("Coludnt find the user");
            appUser.UserAccount = user;
            var res = await userManager.UpdateAsync(appUser);
            if (!res.Succeeded) throw new AuthException("Failed to update the user");
        }

        private string GenerateToken(string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(settings.JwtSecurityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
