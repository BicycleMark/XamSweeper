using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Sweeper.Infrastructure;
using Sweeper.Models;

namespace Sweeper.ViewModels
{
    public class AboutPageViewModel : AppMapViewModelBase, IActiveAware
    {

#pragma warning disable 67
        public event EventHandler IsActiveChanged;
#pragma warning restore 67

        public bool IsActive { get; set; }
        public IAboutModel AboutModel { get; private set; }

        public AboutPageViewModel(INavigationService navigationService, IAboutModel aboutModel) : base (navigationService)
        {
            AboutModel = aboutModel;
        }
    }
}
