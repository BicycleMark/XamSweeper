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
            var sweeperGameModel = new Moq.Mock<ISweeperGameModel>();
            _viewModel = new GamePageViewModel(nav.Object, settingsModel.Object, sweeperGameModel.Object);
        }

        [TestMethod]
        public void TestConstruction()
        {
            var gm = _viewModel.Game;
            var bm = _viewModel.Board;

           
        }

        [TestMethod]
        public void TestIsActive()
        {
            _viewModel.IsActive = true;
            Assert.IsTrue(_viewModel.IsActive == true);
            _viewModel.IsActive = false;
            Assert.IsTrue(_viewModel.IsActive == false);

        }

        [TestMethod]
        public void Test_HasSettings()
        {
            Assert.IsNotNull(_viewModel.Settings );
           

        }
    }
}
