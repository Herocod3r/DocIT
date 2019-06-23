using System;
using Xunit;
namespace DocIT.Tests.ServiceTests
{
    public class GitResolverTests
    {
        public GitResolverTests()
        {
        }

        [Fact]
        public void TestGithub_Path()
        {
            var resolv = DocIT.Core.Services.Git.GitFactory.GetResolver("Github");
            var xpath = resolv.GetFileIdentifier(new Core.Data.Models.GitResolverItem { Branch = "master", RepoName = "TushPay", FilePath = "TushPay.Web/appsettings.json", GitConnection = new Core.Data.Models.GitConnectionConfig { AccountName = "herocod3r", PersonalToken = "10b92dceaf8af90cd47f34410933bede6ffda581" } }).Result;
            Assert.NotNull(xpath);
            Assert.NotEqual("", xpath);
        }

        [Fact]
        public void TestGithub_FileGetter()
        {
            var resolv = DocIT.Core.Services.Git.GitFactory.GetResolver("Github");
            var xpath = resolv.GetFileIdentifier(new Core.Data.Models.GitResolverItem { Branch = "master", RepoName = "TushPay", FilePath = "TushPay.Web/appsettings.json", GitConnection = new Core.Data.Models.GitConnectionConfig { AccountName = "herocod3r", PersonalToken = "10b92dceaf8af90cd47f34410933bede6ffda581" } }).Result;
            Assert.NotNull(xpath);
            Assert.NotEqual("", xpath);

            var file = new System.IO.StreamReader(resolv.GetFileData(xpath).Result).ReadToEndAsync().Result;
            Assert.NotEqual("", file);
        }
    }
}
