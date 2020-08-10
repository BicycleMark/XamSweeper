using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Navigation;
using Sweeper.Infrastructure;
using Sweeper.Models;
using Sweeper.ViewModels;


namespace Sweeper.Test.ViewModel
{
    
    [TestClass]
    public class GamePageViewModelTest
    {
        GamePageViewModel _viewModel;
        [TestInitialize]
        public void Setup()
        {
            // Runs before each test. (Optional)
            var nav = new Moq.Mock<INavigationService>();
            var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            var settingsModel = new Moq.Mock<ISettingsModel>();
            var boardModel = new Moq.Mock<IBoardModel>();
            var gameModel = new Moq.Mock<IGameModel>();
            _viewModel = new GamePageViewModel(nav.Object, settingsModel.Object, boardModel.Object, gameModel.Object);
        }

        [TestMethod]
        public void TestConstruction()
        {
            Assert.Inconclusive();
        }
    }
}
