using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweeper.Models;
using Sweeper.Infrastructure;

namespace Sweeper.Test.Models
{
    [TestClass]
    public class AboutModelTests
    {
        AboutModel _model;
        [TestInitialize]
        [TestMethod]
        public void Setup()
        {
            var appInfo = new Moq.Mock<IAppInfo>();
            appInfo.SetupGet(m => m.BuildString).Returns("BuildString");
            appInfo.SetupGet(m => m.Name).Returns("Name");
            appInfo.SetupGet(m => m.PackageName).Returns("PackageName");
            appInfo.SetupGet(m => m.Version).Returns(new Version(1, 0, 0));
            appInfo.SetupGet(m => m.VersionString).Returns("VersionString");
           


            _model = new AboutModel(appInfo.Object);
        }
        [TestMethod]
        public void TestConstruction()
        {
            Assert.IsNotNull(_model.AppInfo.BuildString);
            Assert.IsNotNull(_model.AppInfo.Name);
            Assert.IsNotNull(_model.AppInfo.PackageName);
            Assert.IsNotNull(_model.AppInfo.Version);
            Assert.IsNotNull(_model.AppInfo.VersionString);

        }
    }
}
