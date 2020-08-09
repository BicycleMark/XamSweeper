using System;
using System.Collections.Generic;
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
    public class SettingsPageViewModel : AppMapViewModelBase, IActiveAware
    {
        IPropertyRepository _propertyRepository;
#pragma warning disable 67
        public event EventHandler IsActiveChanged;
#pragma warning restore 67

        public bool IsActive { get; set; }



        ISettingsModel _settingsModel;
        public ISettingsModel SettingsModel
        {
            get => _settingsModel;
            set { SetProperty(ref _settingsModel, value); }
        }
       
        public SettingsPageViewModel(INavigationService navigationService, ISettingsModel settings) : base (navigationService)
        {
            SettingsModel = settings;
           

        }

        private void _settingsModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }

        public bool Load()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
