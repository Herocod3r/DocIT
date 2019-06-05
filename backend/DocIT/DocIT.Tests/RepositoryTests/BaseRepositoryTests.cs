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
    public class BaseRepositoryTests
    {
        IMongoDatabase database;
        public BaseRepositoryTests()
        {
           var _runner = MongoDbRunner.Start();
            MongoDB.Driver.MongoClient client = new MongoClient(_runner.ConnectionString);
            database = client.GetDatabase("IntegrationTest");
        }
        [Fact]
        public void TestCreateNew()
        {
            IGitConfigurationRepository repo = new GitConfigurationRepository(database);
            var id = Guid.NewGuid();
            var rsp = repo.CreateNewAsync(new GitConnectionConfig { AccountName = "herocod3r", PersonalToken = "tos829", UserId = id }).Result;
            Assert.NotEqual(default(Guid), rsp.Id);
        }


        [Fact]
        public void TestGetById()
        {
            IGitConfigurationRepository repo = new GitConfigurationRepository(database);
            var id = Guid.NewGuid();
            var rsp = repo.CreateNewAsync(new GitConnectionConfig { AccountName = "herocod3r", PersonalToken = "tos829", UserId = id }).Result;
            var xitem = repo.GetByIdAsync(rsp.Id).Result;
            Assert.Equal(rsp.Id, xitem.Id);
        }

        [Fact]
        public void TestUpdate()
        {
            IGitConfigurationRepository repo = new GitConfigurationRepository(database);
            var id = Guid.NewGuid();
            var rsp = repo.CreateNewAsync(new GitConnectionConfig { AccountName = "herocod3r", PersonalToken = "tos829", UserId = id }).Result;
            rsp.AccountName = "jaytee";
            repo.UpdateAsync(rsp).Wait();

            var xitem = repo.GetByIdAsync(rsp.Id).Result;
            Assert.Equal(rsp.AccountName, xitem.AccountName);

        }

        [Fact]
        public void TestDelete()
        {
            IGitConfigurationRepository repo = new GitConfigurationRepository(database);
            var id = Guid.NewGuid();
            var rsp = repo.CreateNewAsync(new GitConnectionConfig { AccountName = "herocod3r", PersonalToken = "tos829", UserId = id }).Result;
            rsp.AccountName = "jaytee";
            repo.DeleteAsync(rsp).Wait();

            var xitem = repo.GetByIdAsync(rsp.Id).Result;
            Assert.Null(xitem);
        }

        [Fact]
        public void TestQuery()
        {
            IGitConfigurationRepository repo = new GitConfigurationRepository(database);
            var id = Guid.NewGuid();
            var rsp = repo.CreateNewAsync(new GitConnectionConfig { AccountName = "herocod3r", PersonalToken = "tos829", UserId = id }).Result;
            var item = repo.QueryAsync().Where(x => x.AccountName == "herocod3r").FirstOrDefault();
            Assert.NotNull(item);
        }
    }
}
