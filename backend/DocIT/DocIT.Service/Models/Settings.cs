using System;
using Microsoft.Extensions.Configuration;

namespace DocIT.Service.Models
{
    public class Settings
    {
        public Settings(IConfiguration configuration)
        {
            var mongoConnectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
            var mailerConnectionString = Environment.GetEnvironmentVariable("MailerCon");
            var dbName = Environment.GetEnvironmentVariable("DBName");


            MongoConnectionString = string.IsNullOrEmpty(mongoConnectionString) ? "mongodb://localhost:27017" : mongoConnectionString;
            MailerConnectionString = string.IsNullOrWhiteSpace(mailerConnectionString) ? "" : mailerConnectionString;
            dbName = string.IsNullOrEmpty(dbName) ? "DocITDB" : dbName;
            JwtIssuer = configuration.GetSection("JwtIssuerOptions:Issuer").Value;
            JwtAudience = configuration.GetSection("JwtIssuerOptions:Audience").Value;
            JwtSecurityKey = configuration.GetSection("JwtIssuerOptions:SecurityKey").Value;
            System.Diagnostics.Debug.WriteLine(mongoConnectionString + "   " + dbName);
            Console.WriteLine(mongoConnectionString + "   " + dbName);
        }

        public string MongoConnectionString { get; private set; }
        public string MailerConnectionString { get; private set; }
        public string JwtIssuer { get; private set; } 
        public string JwtAudience { get; private set; } 
        public string JwtSecurityKey { get; private set; }
        public string DbName { get;private set; }
    }
}
