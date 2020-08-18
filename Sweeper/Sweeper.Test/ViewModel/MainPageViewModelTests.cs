using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Navigation;
using Sweeper.ViewModels;

namespace Sweeper.Test.ViewModel
{
    [TestClass]
    public class MainPageViewModelTests
    {
        MainPageViewModel _viewModel;
        [TestInitialize]
        public void SetUp()
        {
            var nav = new Moq.Mock<INavigationService>();
            _viewModel = new MainPageViewModel(nav.Object);
        }
        [TestMethod]
        public void TestConstruction()
        {
           
        }
    }
}
