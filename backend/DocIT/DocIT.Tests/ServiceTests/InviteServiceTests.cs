using System;
using Mongo2Go;
using MongoDB.Driver;
using Xunit;
using DocIT.Core.Data.Models;
using DocIT.Core.Repositories;
using DocIT.Core.Repositories.Implementations;
using DocIT.Core.Services;
using DocIT.Core.Services.Implementations;


namespace DocIT.Tests.ServiceTests
{
    public class InviteServiceTests : IDisposable
    {
        IMongoDatabase database;
        MongoDbRunner _runner;
        public InviteServiceTests()
        {
            DocIT.Core.AutoMapperConfig.RegisterMappings();

            _runner = MongoDbRunner.Start();
            MongoDB.Driver.MongoClient client = new MongoClient(_runner.ConnectionString);
            database = client.GetDatabase("IntegrationTest");
        }

        [Fact]
        public void Test_Create_Invite()
        {
            var userRepo = new UserRepository(database);
            var usr = userRepo.CreateNew(new User { DateCreated = DateTime.Now, Email = "jethromain@gmail.com", Name = "Jaytee" });
            var invRepo = new InviteRepository(database, userRepo);
            var project = invRepo.CreateNew(new Project { CreatedByUserId = usr.Id, DateCreated = DateTime.Now, Description = "A demo api", Name = "DEMO API" });
            var service = new InviteService(invRepo);

            var res = service.CreateInvite(new Core.Data.Payloads.InvitePayload { Email = "mike@gmail.com", ProjectId = project.Id }, Guid.NewGuid()).Result;
            Assert.NotNull(res);
           
        }

        [Fact]
        public void Test_GetUser_Invites_Success()
        {
            var userRepo = new UserRepository(database);
            var usr = userRepo.CreateNew(new User { DateCreated = DateTime.Now, Email = "jethromain@gmail.com", Name = "Jaytee" });
            var invRepo = new InviteRepository(database, userRepo);
            var project = invRepo.CreateNew(new Project { CreatedByUserId = usr.Id, DateCreated = DateTime.Now, Description = "A demo api", Name = "DEMO API" });
            var service = new InviteService(invRepo);

            var res = service.CreateInvite(new Core.Data.Payloads.InvitePayload { Email = "mike@gmail.com", ProjectId = project.Id }, Guid.NewGuid()).Result;
            Assert.NotNull(res);
            var ivts = service.GetUserInvites("mike@gmail.com", 0, 30).Result;
            Assert.NotNull(ivts);
            Assert.NotEmpty(ivts.Result);
        }


        [Fact]
        public void Test_Create_Invite_NotFound()
        {
            var userRepo = new UserRepository(database);
            var usr = userRepo.CreateNew(new User { DateCreated = DateTime.Now, Email = "jethromain@gmail.com", Name = "Jaytee" });
            var invRepo = new InviteRepository(database, userRepo);
            var project = invRepo.CreateNew(new Project { CreatedByUserId = usr.Id, DateCreated = DateTime.Now, Description = "A demo api", Name = "DEMO API" });
            var service = new InviteService(invRepo);

            Assert.ThrowsAsync<DocIT.Core.Services.Exceptions.InviteException>(async () => await service.CreateInvite(new Core.Data.Payloads.InvitePayload { Email = "mike@gmail.com", ProjectId = Guid.NewGuid()}, Guid.NewGuid()));
            
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
            Dispose(true);
        }
        #endregion

    }
}
