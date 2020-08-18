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
    namespace ErrorClasses
    {
        class ErrorEmptySourceProvider : ISettingsProvider
        {
            public string ThemeSource { get { return ""; } }

            public string DefinitionsSource
            {
                get { return ""; }
            }
        }

        class ErrorNullSourceProvider : ISettingsProvider
        {
            public string ThemeSource { get { return null; } }

            public string DefinitionsSource
            {
                get { return null; }
            }
        }

        class NullThemeSourceProvider : ISettingsProvider
        {
            public string ThemeSource { get { return null; } }

            public string DefinitionsSource
            {
                get { return "BEGINNER,10,10,10|INTERMEDIATE,15,15,15|ADVANCED,20,20,20|CUSTOM,25,25,25"; }
            }
        }

        class WrongNumberOfDefsThemeSourceProvider : ISettingsProvider
        {
            public string ThemeSource { get { return "Default, Chocolate, Copper, Key West, Powder Puff"; } }

            public string DefinitionsSource
            {
                get { return "INTERMEDIATE,15,15|ADVANCED,20,20,20|CUSTOM,25,25,25"; }
            }
        }

        class WrongNumberOfThemesSourceProvider : ISettingsProvider
        {
            public string ThemeSource { get { return "Default"; } }

            public string DefinitionsSource
            {
                get { return "BEGINNER,10,10,10|INTERMEDIATE,15,15,15|ADVANCED,20,20,20|CUSTOM,25,25,25"; }
            }
        }

        class NullDefinitionSourceProvider : ISettingsProvider
        {
            public string ThemeSource { get { return "Default, Chocolate, Copper, Key West, Powder Puff"; } }

            public string DefinitionsSource
            {
                get { return null; }
            }
        }
    }
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
            var provider = new ResouceSettingsProvider();
            _settingsModel = new SettingsModel(repo.Object,provider);
        }

        [TestMethod]
        public void TestConstruction()
        {
            Assert.IsTrue(_settingsModel.Themes.Count > 0);
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
            _settingsModel.SelectedGameDefinition = _settingsModel.GameDefinitions.FirstOrDefault(m=>m.Type== gameType);
            Assert.AreEqual(gameType, _settingsModel.SelectedGameDefinition.Type);
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

        [TestMethod]
        public void Test_ErrorEmptySourceProvider()
        {
            var testPassed = false;
            var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            var provider = new ErrorClasses.ErrorEmptySourceProvider();
            try
            {
                _settingsModel = new SettingsModel(repo.Object, provider);
            }catch (ArgumentException)
            {
                testPassed = true;

            }
            Assert.IsTrue(testPassed);
        }

        [TestMethod]
        public void Test_ErrorNullSourceProvider()
        {
            var testPassed = false;
            var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            var provider = new ErrorClasses.ErrorNullSourceProvider();
            try
            {
                _settingsModel = new SettingsModel(repo.Object, provider);
            }
            catch (ArgumentException)
            {
                testPassed = true;

            }
            Assert.IsTrue(testPassed);
        }

        [TestMethod]
        public void Test_NullDefinitionSourceProvider()
        {
            var testPassed = false;
            var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            var provider = new ErrorClasses.NullDefinitionSourceProvider();
            try
            {
                _settingsModel = new SettingsModel(repo.Object, provider);
            }
            catch (ArgumentException)
            {
                testPassed = true;

            }
            Assert.IsTrue(testPassed);
        }

        [TestMethod]
        public void Test_NullThemeSourceProvider()
        {
            var testPassed = false;
            var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            var provider = new ErrorClasses.NullThemeSourceProvider();
            try
            {
                _settingsModel = new SettingsModel(repo.Object, provider);
            }
            catch (ArgumentException)
            {
                testPassed = true;

            }
            Assert.IsTrue(testPassed);
        }

        [TestMethod]
        public void Test_WrongNumberOfDefsThemeSourceProvider()
        {
            var testPassed = false;
            var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            var provider = new ErrorClasses.WrongNumberOfDefsThemeSourceProvider();
            try
            {
                _settingsModel = new SettingsModel(repo.Object, provider);
            }
            catch (ArgumentException)
            {
                testPassed = true;

            }
            Assert.IsTrue(testPassed);
        }

        [TestMethod]
        public void Test_WrongNumberOfThemesSourceProvider()
        {
            var testPassed = false;
            var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            var provider = new ErrorClasses.WrongNumberOfThemesSourceProvider();
            try
            {
                _settingsModel = new SettingsModel(repo.Object, provider);
            }
            catch (ArgumentException)
            {
                testPassed = true;

            }
            Assert.IsTrue(testPassed);
        }

        [DataRow("Default")]
        [DataRow("Chocolate")]
        [DataRow("Copper")]
        [DataRow("Key West")]
        [DataRow("Powder Puff")]
        [DataTestMethod()] 
        public void TestChangeTheme(string expectedTheme)
        {
            _settingsModel.CurrentTheme = expectedTheme;
            Assert.AreEqual(expectedTheme, _settingsModel.CurrentTheme);         
        }

        [TestMethod()]
        public void TestChangeDisableTimer()
        {
            Assert.IsFalse(_settingsModel.DisableTimerUpdatesForTesting);     
        }
    }
  
}
