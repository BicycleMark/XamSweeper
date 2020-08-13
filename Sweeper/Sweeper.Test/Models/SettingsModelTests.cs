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
        ISettingsModel _settingsModel;
        [TestInitialize]
        public void Setup()
        {
            // Runs before each test. (Optional)
            var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            _settingsModel = new SettingsModel(repo.Object);
        }
        [TestMethod]
        public void TestConstruction()
        {
            
            Assert.IsTrue(_settingsModel.ThemeNames.Count > 0);
            Assert.IsTrue(_settingsModel.CurrentTheme == "Default");
            Assert.IsTrue(_settingsModel.Rows == 10);
            Assert.IsTrue(_settingsModel.Columns == 10);
            Assert.IsTrue(_settingsModel.MineCount == 10);
           
        }
        
        [DataRow(GameTypes.BEGINNER, 10, 10, 10)]
        [DataRow(GameTypes.INTERMEDIATE, 15, 15, 15)]
        [DataRow(GameTypes.ADVANCED, 20, 20, 20)]
        [DataRow(GameTypes.CUSTOM, 25, 25, 25)]
        [DataTestMethod()]
        public void TestChangeGameType(GameTypes gameType, int expectedRows, int expectedColumns, int expectedMines)
        {
            _settingsModel.SelectedGameType = gameType;
            Assert.AreEqual(gameType, _settingsModel.SelectedGameType);
            Assert.AreEqual(expectedRows, _settingsModel.Rows);
            Assert.AreEqual(expectedColumns, _settingsModel.Columns);
            Assert.AreEqual(expectedMines, _settingsModel.MineCount);
            if (gameType == GameTypes.CUSTOM)
            {
                Assert.AreEqual(expectedRows, _settingsModel.CustomRows);
                Assert.AreEqual(expectedColumns, _settingsModel.CustomColumns);
                Assert.AreEqual(expectedMines, _settingsModel.CustomMines);
            }

        }
        [DataRow(0,"Default")]
        [DataRow(1,"Chocolate")]
        [DataRow(2,"Copper")]
        [DataRow(3,"Key West")]
        [DataRow(4,"Powder Puff")]
        [DataTestMethod()]

        
        public void TestChangeTheme(int ndxToSet, string expectedTheme)
        {
            _settingsModel.CurrentThemeIndex = ndxToSet;
            Assert.IsTrue(ndxToSet == _settingsModel.CurrentThemeIndex);
            Assert.Inconclusive();
            //Assert.IsTrue(_settingsModel.CurrentTheme.Equals( expectedTheme));
        }

        [TestMethod()]
        public void TestChangeDisableTimer()
        {
            Assert.IsFalse(_settingsModel.DisableTimerUpdatesForTesting);
           
        }
    }
  
}
