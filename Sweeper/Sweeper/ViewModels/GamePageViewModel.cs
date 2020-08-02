using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Sweeper.Infrastructure;

namespace Sweeper.ViewModels
{
    public class GamePageViewModel : AppMapViewModelBase, IActiveAware
    {

#pragma warning disable 67
        public event EventHandler IsActiveChanged;
#pragma warning restore 67

        public bool IsActive { get; set; }

        SettingsPageViewModel
        public GamePageViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
