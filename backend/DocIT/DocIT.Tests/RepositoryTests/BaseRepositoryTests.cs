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
    public class BaseRepositoryTests : IDisposable
    {
        IMongoDatabase database;
        MongoDbRunner _runner;
        public BaseRepositoryTests()
        {

            _runner = MongoDbRunner.Start();
            MongoDB.Driver.MongoClient client = new MongoClient(_runner.ConnectionString);
            database = client.GetDatabase("IntegrationTest");
        }
        [Fact]
        public void TestCreateNew()
        {
            IGitConfigurationRepository repo = new GitConfigurationRepository(database);
            var id = Guid.NewGuid();
            var rsp = repo.CreateNew(new GitConnectionConfig { AccountName = "herocod3r", PersonalToken = "tos829", UserId = id });
            Assert.NotEqual(default(Guid), rsp.Id);
        }


        [Fact]
        public void TestGetById()
        {
            IGitConfigurationRepository repo = new GitConfigurationRepository(database);
            var id = Guid.NewGuid();
            var rsp = repo.CreateNew(new GitConnectionConfig { AccountName = "herocod3r", PersonalToken = "tos829", UserId = id });
            var xitem = repo.GetById(rsp.Id);
            Assert.Equal(rsp.Id, xitem.Id);
        }

        [Fact]
        public void TestUpdate()
        {
            IGitConfigurationRepository repo = new GitConfigurationRepository(database);
            var id = Guid.NewGuid();
            var rsp = repo.CreateNew(new GitConnectionConfig { AccountName = "herocod3r", PersonalToken = "tos829", UserId = id });
            rsp.AccountName = "jaytee";
            repo.Update(rsp);

            var xitem = repo.GetById(rsp.Id);
            Assert.Equal(rsp.AccountName, xitem.AccountName);

        }

        [Fact]
        public void TestDelete()
        {
            IGitConfigurationRepository repo = new GitConfigurationRepository(database);
            var id = Guid.NewGuid();
            var rsp = repo.CreateNew(new GitConnectionConfig { AccountName = "herocod3r", PersonalToken = "tos829", UserId = id });
            rsp.AccountName = "jaytee";
            repo.Delete(rsp);

            var xitem = repo.GetById(rsp.Id);
            Assert.Null(xitem);
        }

        [Fact]
        public void TestQuery()
        {
            IGitConfigurationRepository repo = new GitConfigurationRepository(database);
            var id = Guid.NewGuid();
            var rsp = repo.CreateNew(new GitConnectionConfig { AccountName = "herocod3r", PersonalToken = "tos829", UserId = id });
            var item = repo.QueryAsync().Where(x => x.AccountName == "herocod3r").FirstOrDefault();
            Assert.NotNull(item);
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
