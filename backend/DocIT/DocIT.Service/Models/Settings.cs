using System;
using Microsoft.Extensions.Configuration;

namespace DocIT.Service.Models
{
    public class Settings
    {
        public Settings(IConfiguration configuration)
        {
            var mongoConnectionString = Environment.GetEnvironmentVariable("MonogoCon");
            var mailerConnectionString = Environment.GetEnvironmentVariable("MailerCon");


            MongoConnectionString = string.IsNullOrEmpty(mongoConnectionString) ? "" : mongoConnectionString;
            MailerConnectionString = string.IsNullOrWhiteSpace(mailerConnectionString) ? "" : mailerConnectionString;
            JwtIssuer = configuration.GetSection("JwtIssuerOptions:Issuer").Value;
            JwtAudience = configuration.GetSection("JwtIssuerOptions:Audience").Value;
            JwtSecurityKey = configuration.GetSection("JwtIssuerOptions:SecurityKey").Value;
        }

        public string MongoConnectionString { get; private set; }
        public string MailerConnectionString { get; private set; }
        public string JwtIssuer { get; private set; } 
        public string JwtAudience { get; private set; } 
        public string JwtSecurityKey { get; private set; } 
    }
}
