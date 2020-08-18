using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Navigation;
using Sweeper.Infrastructure;
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
            var aboutmodel = new Moq.Mock<IAboutModel>();
            _viewModel = new AboutPageViewModel(nav.Object,aboutmodel.Object);
        }
        [TestMethod]
        public void TestConstruction()
        {
            //Assert.IsNotNull(_viewModel.)
            Assert.IsNotNull(_viewModel.AboutModel);
        }

        [TestMethod]
        public void TestIsActive()
        {
            _viewModel.IsActive = true;
            Assert.IsTrue(_viewModel.IsActive == true);
            _viewModel.IsActive = false;
            Assert.IsTrue(_viewModel.IsActive == false);
        }
    }
}