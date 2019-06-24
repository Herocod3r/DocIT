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
   
    public class ProjectRepositoryTests : IDisposable
    {
        IMongoDatabase database;
        MongoDbRunner _runner;
        public ProjectRepositoryTests()
        {
            _runner = MongoDbRunner.Start();
            MongoDB.Driver.MongoClient client = new MongoClient(_runner.ConnectionString);
            database = client.GetDatabase("IntegrationTest");
        }


        [Fact]
        public void TestGetAllForUser()
        {
            var repo = new ProjectRepository(database);
            var uid = Guid.NewGuid();
            repo.CreateNew(new Project { CreatedByUserId = uid, Name = "Sample project", Description = "A sample project" });
            var (col,_) = repo.GetAllForUser(0, 100, uid,"");
            Assert.NotNull(col);
            Assert.NotEmpty(col);
            
        }

        [Fact]
        public void TestGetSIngleForUser()
        {
            var repo = new ProjectRepository(database);
            var uid = Guid.NewGuid();
            var xr = repo.CreateNew(new Project { CreatedByUserId = uid, Name = "Sample project", Description = "A sample project" });
            var item = repo.GetSingleForUser(xr.Id, uid,"");
            Assert.NotNull(item);
        }

        [Fact]
        public void TestGetAllSubProjects()
        {
            var repo = new ProjectRepository(database);
            var uid = Guid.NewGuid();
            var pid = Guid.NewGuid();
             repo.CreateNew(new Project { CreatedByUserId = uid, Name = "Sample project", Description = "A sample project", ParentId = pid });

            var col = repo.GetAllSubProjects(pid);
            Assert.NotNull(col);
            Assert.NotEmpty(col);
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
