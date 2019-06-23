using System;
using System.Collections.Generic;
using DocIT.Core.Data.Models;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Linq.Expressions;
using DocIT.Core.Repositories.Implementations;
using DocIT.Core.Repositories;

namespace DocIT.Tests.RepositoryTests
{
    public class MockUserRepository : BaseRepository<User, User, Guid>, IUserRepository
    {
        public MockUserRepository(IMongoDatabase db): base(db)
        {
        }

        public User CreateUserAccount(User user, string password)
        {
            user.Id = Guid.NewGuid();
            return user;
        }

        public User FindUserByEmailAndPassword(string email, string password)
        {
            return new User { Email = email,Id = Guid.NewGuid() };
        }

        protected override IQueryable<User> ProjectedSource => Collection.AsQueryable();
    }
}
