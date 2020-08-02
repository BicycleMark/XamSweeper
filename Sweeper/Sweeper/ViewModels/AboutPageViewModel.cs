using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Sweeper.Infrastructure;

namespace Sweeper.ViewModels
{
    public class AboutPageViewModel : AppMapViewModelBase, IActiveAware
    {

#pragma warning disable 67
        public event EventHandler IsActiveChanged;
#pragma warning restore 67

        public bool IsActive { get; set; }

        public AboutPageViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
