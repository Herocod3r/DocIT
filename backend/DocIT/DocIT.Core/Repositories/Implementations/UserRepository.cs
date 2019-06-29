using System;
using DocIT.Core.Data.Models;
using MongoDB.Driver;

namespace DocIT.Core.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User,User,Guid>, IUserRepository
    {
        public UserRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
