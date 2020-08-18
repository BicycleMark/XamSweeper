using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Navigation;
using Sweeper.Infrastructure;
using Sweeper.ViewModels;


namespace Sweeper.Test.ViewModel
{
    [TestClass]
    public class SettingsPageViewModelTest
    {
        SettingsPageViewModel _viewModel;
        [TestInitialize]
        public void Setup()
        {
            // Runs before each test. (Optional)
            var nav = new Moq.Mock<INavigationService>();
            var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            var settingsModel = new Moq.Mock<ISettingsModel>(); 
            _viewModel = new SettingsPageViewModel(nav.Object, settingsModel.Object);
        }

        [TestMethod]
        public void TestConstruction()
        {
            Assert.IsTrue(_viewModel.SettingsModel != null);
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
        public void TestHasSettings()
        {
            Assert.IsTrue(_viewModel.SettingsModel != null);
           
        }
    }
}
