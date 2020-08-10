using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweeper.Infrastructure;
using Sweeper.Models;
using Sweeper.Models.Game;

namespace Sweeper.Test.Models
{
    [TestClass]
    public class GameModelTest
    {
        IGameModel _model;
        [TestInitialize]
        public void Setup()
        {
            var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            var settingsModel = new Moq.Mock<ISettingsModel>();
            var boardModel = new Moq.Mock<IBoardModel>();
            _model = new GameModel(repo.Object, settingsModel.Object, boardModel.Object);
        }
        [TestMethod]
        public void  TestConstruction()
        {
            Assert.AreEqual(0, _model.GameTime);
            Thread.Sleep(1500);
            Assert.IsTrue(_model.GameTime > 0);
        }
    }
}
