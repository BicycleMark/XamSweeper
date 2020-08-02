using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Sweeper.Infrastructure;
using Sweeper.Models;

namespace Sweeper.ViewModels
{
    public class SettingsPageViewModel : AppMapViewModelBase, IActiveAware
    {

#pragma warning disable 67
        public event EventHandler IsActiveChanged;
#pragma warning restore 67

        public bool IsActive { get; set; }

        public SettingsModel Model;

        public SettingsPageViewModel(INavigationService navigationService) : base (navigationService)
        {
            Model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }
    }
}
