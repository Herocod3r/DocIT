using System;
using DocIT.Core.Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace DocIT.Core.Repositories
{
    public interface IUserRepository : IBaseRepository<User,User,Guid>
    {

    }
}
