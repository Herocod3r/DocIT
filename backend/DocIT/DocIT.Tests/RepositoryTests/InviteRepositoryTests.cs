using System;
using Mongo2Go;
using MongoDB.Driver;
using Xunit;
using DocIT.Core.Data.Models;
using DocIT.Core.Repositories;
using DocIT.Core.Repositories.Implementations;
using System.Linq;

namespace DocIT.Tests.RepositoryTests
{
    public class InviteRepositoryTests : IDisposable
    {
        IMongoDatabase database;
        MongoDbRunner _runner;
        public InviteRepositoryTests()
        {
            _runner = MongoDbRunner.Start();
            MongoDB.Driver.MongoClient client = new MongoClient(_runner.ConnectionString);
            database = client.GetDatabase("IntegrationTest");
        }

        [Fact]
        public void TestQuery()
        {
            var userRepo = new UserRepository(database);
            var invRepo = new InviteRepository(database,userRepo);
            invRepo.QueryAsync().FirstOrDefault();
        }

        [Fact]
        public void TestCreateInvite()
        {

            var userRepo = new UserRepository(database);
            var usr = userRepo.CreateNew(new User { DateCreated = DateTime.Now, Email = "jethromain@gmail.com", Name = "Jaytee" });
            var invRepo = new InviteRepository(database, userRepo);
            var project = invRepo.CreateNew(new Project { CreatedByUserId = usr.Id, DateCreated = DateTime.Now, Description = "A demo api", Name = "DEMO API" });
            var ivc = invRepo.CreateInvite(new Invite { Email = "mikey@gmail.com", InvitedAt = DateTime.Now }, project.Id,usr.Id);
            Assert.NotNull(ivc);
            Assert.NotEmpty(ivc.Invites);
        }

        [Fact]
        public void TestGetUserInvites()
        {
            var userRepo = new UserRepository(database);
            var usr = userRepo.CreateNew(new User { DateCreated = DateTime.Now, Email = "jethromain@gmail.com", Name = "Jaytee" });
            var invRepo = new InviteRepository(database, userRepo);
            var project = invRepo.CreateNew(new Project { CreatedByUserId = usr.Id, DateCreated = DateTime.Now, Description = "A demo api", Name = "DEMO API" });
            var ivc = invRepo.CreateInvite(new Invite { Email = "mikey@gmail.com", InvitedAt = DateTime.Now }, project.Id,usr.Id);
            Assert.NotNull(ivc);
            Assert.NotEmpty(ivc.Invites);


            var (invts,_) = invRepo.GetUserInvites("mikey@gmail.com", 0, 30);
            Assert.NotEmpty(invts);

        }

        [Fact]
        public void TestDeleteUser()
        {
            var userRepo = new UserRepository(database);
            var usr = userRepo.CreateNew(new User { DateCreated = DateTime.Now, Email = "jethromain@gmail.com", Name = "Jaytee" });
            var invRepo = new InviteRepository(database, userRepo);
            var project = invRepo.CreateNew(new Project { CreatedByUserId = usr.Id, DateCreated = DateTime.Now, Description = "A demo api", Name = "DEMO API" });
            var ivc = invRepo.CreateInvite(new Invite { Email = "mikey@gmail.com", InvitedAt = DateTime.Now }, project.Id,usr.Id);
            Assert.NotNull(ivc);
            Assert.NotEmpty(ivc.Invites);

            invRepo.DeleteInvite(new Invite { Email = "mikey@gmail.com", InvitedAt = DateTime.Now }, project.Id,usr.Id);
            var vproject = invRepo.GetById(project.Id);
            Assert.NotEqual(ivc.Invites.Count, vproject.Invites.Count);
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DocIT.Core.AutoMapperConfig.Clear();
                    _runner.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
