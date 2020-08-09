using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweeper.Infrastructure;
using Sweeper.Models;

namespace Sweeper.Test.Models
{
    [TestClass]
    public class SettingsModelTest
    {
        [TestMethod]
        public void TestConstruction()
        {
            var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            ISettingsModel sm = new SettingsModel(repo.Object);
            Assert.IsTrue(sm.ThemeNames.Count > 0);


        }
        [TestMethod()]
        public void TestTODO()
        {
            
            Assert.Inconclusive();


        }
    }
  
}
