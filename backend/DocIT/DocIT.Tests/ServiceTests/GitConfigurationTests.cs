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
    public class GitConfigurationTests : IDisposable
    {
        IMongoDatabase database;
        MongoDbRunner _runner;
        public GitConfigurationTests()
        {
            DocIT.Core.AutoMapperConfig.RegisterMappings();
           
            _runner = MongoDbRunner.Start();
            MongoDB.Driver.MongoClient client = new MongoClient(_runner.ConnectionString);
            database = client.GetDatabase("IntegrationTest");
        }


       

        [Fact]
        public void TestCreateGitConfig()
        {
            var serv = new GitConfigurationService(new GitConfigurationRepository(database));
            var item = serv.CreateNewAsync(Guid.NewGuid(), new Core.Data.Payloads.GitConfigPayload { AccountName = "jaytee", PersonalToken = "token", Type = "Github" }).Result;
            Assert.NotNull(item);
            Assert.NotEqual(default(Guid), item.Id);
        }

        [Fact]
        public void TestListAll()
        {
            var serv = new GitConfigurationService(new GitConfigurationRepository(database));
            var uid = Guid.NewGuid();
            var val = serv.CreateNewAsync(uid, new Core.Data.Payloads.GitConfigPayload { AccountName = "jaytee", PersonalToken = "token", Type = "Github" }).Result;
            var items = serv.ListAllForUser(uid);
            Assert.NotEmpty(items.Result);
        }

        [Fact]
        public void TestDeleteAsync()
        {
            var serv = new GitConfigurationService(new GitConfigurationRepository(database));
            var uid = Guid.NewGuid();
            var item = serv.CreateNewAsync(uid, new Core.Data.Payloads.GitConfigPayload { AccountName = "jaytee", PersonalToken = "token", Type = "Github" }).Result;
            serv.DeleteAsync(uid,item.Id).Wait();
            Assert.ThrowsAsync<DocIT.Core.Services.Exceptions.GitConfigException>(async ()=> await serv.GetById(item.Id,uid));
           
        }

        [Fact]
        public void TestGetById()
        {
            var serv = new GitConfigurationService(new GitConfigurationRepository(database));
            var uid = Guid.NewGuid();
            var item = serv.CreateNewAsync(uid, new Core.Data.Payloads.GitConfigPayload { AccountName = "jaytee", PersonalToken = "token", Type = "Github" }).Result;
             var itm = serv.GetById(item.Id, uid).Result;
            Assert.NotNull(itm);
            Assert.Equal(item.Id, itm.Id);
        }

        [Fact]
        public void TestUpdate()
        {
            var serv = new GitConfigurationService(new GitConfigurationRepository(database));
            var uid = Guid.NewGuid();
            var item = serv.CreateNewAsync(uid, new Core.Data.Payloads.GitConfigPayload { AccountName = "jaytee", PersonalToken = "token", Type = "Github" }).Result;
            serv.UpdateAsync(uid, item.Id, new Core.Data.Payloads.GitConfigPayload { AccountName = "jaytee", PersonalToken = "token", Type = "Bit" }).Wait();
            var itm = serv.GetById(item.Id, uid).Result;
            Assert.Equal("Bit",itm.Type);
            Assert.Equal(item.Id, itm.Id);
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
