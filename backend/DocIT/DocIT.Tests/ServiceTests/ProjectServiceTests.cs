using System;
using Mongo2Go;
using MongoDB.Driver;
using Xunit;
using DocIT.Core.Data.Models;
using DocIT.Core.Repositories;
using DocIT.Core.Repositories.Implementations;
using DocIT.Core.Services;
using DocIT.Core.Services.Implementations;
using System.Linq;


namespace DocIT.Tests.ServiceTests
{
    public class ProjectServiceTests : IDisposable
    {
        IMongoDatabase database;
        MongoDbRunner _runner;
        ProjectService service;
        Guid uid;
        ProjectRepository pr;
        public ProjectServiceTests()
        {
            uid = Guid.NewGuid();
            DocIT.Core.AutoMapperConfig.RegisterMappings();
            _runner = MongoDbRunner.Start();
            MongoDB.Driver.MongoClient client = new MongoClient(_runner.ConnectionString);
            database = client.GetDatabase("IntegrationTest");
            pr = new ProjectRepository(database);
            service = new ProjectService(pr);
            
        }

        [Fact]
        public void Test_CreateProject()
        {
            var rsp = service.CreateProject(new Core.Data.Payloads.ProjectPayload { Description = "An app that can fly", Name = "Fly Api", SwaggerUrl = "sawggenrunds832" }, uid).Result;
            Assert.NotNull(rsp);
        }

        [Fact]
        public void TestListAllProject()
        {
            Test_CreateProject();
            var rsp = service.ListAll(uid, "", "", "").Result;
            Assert.NotNull(rsp);
            Assert.NotEmpty(rsp.Result);
        }

        [Fact]
        public void Test_GenerateProjectLink()
        {
            var rsp = service.CreateProject(new Core.Data.Payloads.ProjectPayload { Description = "An app that can fly", Name = "Fly Api", SwaggerUrl = "sawggenrunds832" }, uid).Result;
            var link = service.GenerateProjectLink(rsp.Id, uid).Result;
            Assert.NotNull(link);
            Assert.NotEmpty(link.PreviewLinks);
            Assert.NotNull(rsp);
        }

        [Fact]
        public void Test_GetProjectByLink()
        {
            var rsp = service.CreateProject(new Core.Data.Payloads.ProjectPayload { Description = "An app that can fly", Name = "Fly Api", SwaggerUrl = "sawggenrunds832" }, uid).Result;
            var link = service.GenerateProjectLink(rsp.Id, uid).Result;
            
            var item = service.GetProjectByLink(link.PreviewLinks.FirstOrDefault(),uid,"").Result;
            Assert.NotNull(item);
        }

        [Fact]
        public void Test_DeleteProjectByLink()
        {
            var rsp = service.CreateProject(new Core.Data.Payloads.ProjectPayload { Description = "An app that can fly", Name = "Fly Api", SwaggerUrl = "sawggenrunds832" }, uid).Result;
            var link = service.GenerateProjectLink(rsp.Id, uid).Result;

             service.DeleteProjectLink(rsp.Id, uid, link.PreviewLinks.FirstOrDefault()).Wait();
            Assert.ThrowsAsync<ArgumentException>(async ()=> await service.GetProjectByLink(link.PreviewLinks.FirstOrDefault(), uid, ""));
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
