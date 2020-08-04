using System;
using System.Linq;
using Prism;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Unity;
using Sweeper.Infrastructure;
using Sweeper.Models;

namespace Sweeper.ViewModels
{
    public class GamePageViewModel : AppMapViewModelBase, IActiveAware
    {

#pragma warning disable 67
        public event EventHandler IsActiveChanged;
#pragma warning restore 67

        public bool IsActive { get; set; }

        public GamePageViewModel(INavigationService navigationService, ISettings settingsModel ) : base (navigationService)
        {
            var settings = settingsModel;
        }
    }
}
