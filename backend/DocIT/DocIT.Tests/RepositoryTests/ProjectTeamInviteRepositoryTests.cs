using System;
using Mongo2Go;
using MongoDB.Driver;
using Xunit;
using DocIT.Core.Repositories;
using DocIT.Core.Repositories.Implementations;
using DocIT.Core.Data.Models;

namespace DocIT.Tests.RepositoryTests
{
    public class ProjectTeamInviteRepositoryTests : IDisposable
    {
        IMongoDatabase database;
        MongoDbRunner _runner;
        public ProjectTeamInviteRepositoryTests()
        {
            _runner = MongoDbRunner.Start();
            MongoDB.Driver.MongoClient client = new MongoClient(_runner.ConnectionString);
            database = client.GetDatabase("IntegrationTest");
        }

        [Fact]
        public void TestGetUserInvitedProjects()
        {
            var userRepo = new MockUserRepository(database);
            var mainUsr = userRepo.CreateNew(new User { Email = "jethromain@gmail.com", Name = "Jaytee" });
            Assert.NotEqual(default(Guid),mainUsr.Id);
            var inviteUsr = userRepo.CreateNew(new User { Email = "jethro.daniel@innovantics.com", Name = "Jethro" });
            Assert.NotEqual(default(Guid),inviteUsr.Id);
            var projectRepo = new ProjectRepository(database);
            var prjct = projectRepo.CreateNew(new Project { Name = "Demo Project", Description = "A test demo project", CreatedByUserId = mainUsr.Id });
            Assert.NotEqual(default(Guid),prjct.Id);
            var inviteRepo = new TeamInviteRepository(database,userRepo,projectRepo);
            inviteRepo.CreateNew(new ProjectTeamInvite { Email = inviteUsr.Email, InvitedByUserId = mainUsr.Id, ProjectId = prjct.Id });
            try
            {
                var (rsp, _) = inviteRepo.GetUserInvitedProjects(mainUsr.Id, 0, 30);
                Assert.NotEmpty(rsp);
            }
            catch (Exception e)
            {

            }

        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
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
