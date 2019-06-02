using System;
using AspNetCore.Identity.MongoDbCore.Models;

namespace DocIT.Service.Models
{
    public class ApplicationRole : MongoIdentityRole<Guid>
    {
        public ApplicationRole() : base()
        {
        }
        public ApplicationRole(string roleName):base(roleName)
        {

        }
    }
}
