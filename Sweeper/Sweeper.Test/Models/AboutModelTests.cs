using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _model = new AboutModel();
        }
        public void TestConstruction()
        {
            Assert.Inconclusive();
        }
    }
}
