﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Navigation;
using Sweeper.ViewModels;


namespace Sweeper.Test.ViewModel
{
    [TestClass]
    public class AboutPageViewModelTests
    {
        AboutPageViewModel _viewModel;
        [TestInitialize]
        public void SetUp()
        {
            var nav = new Moq.Mock<INavigationService>();
            _viewModel = new AboutPageViewModel(nav.Object);
        }
        [TestMethod]
        public void TestConstruction()
        {
            Assert.Inconclusive();
        }
    }
}