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

namespace DocIT.Service.Authentication
{
    public class TokenService : IUserAuthTokenService
    {
        private readonly Models.Settings settings;

        public TokenService(Models.Settings settings)
        {
            this.settings = settings;
        }

        public string GenerateToken(params (string,object)[] bodyItems)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(settings.JwtSecurityKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(bodyItems.Select(x=>new Claim(x.Item1,x.Item2.ToString())).ToArray()),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
