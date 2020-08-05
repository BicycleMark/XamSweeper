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

        public int Rows
        {
            get { return _settingsModel.Rows; }
            set { _settingsModel.Rows = value; }
        }
        public int Columns
        {
            get { return _settingsModel.Columns; }
            set { _settingsModel.Columns = value; }
        }
        // TODO
    //    public List<string>GameTypes{
    //        get {
    //            return _settingsModel.GameTypes

    //        }
    //}

        public int MineCount 
        {
            get {
                return _settingsModel.MineCount;
                }
            set
            {
                _settingsModel.MineCount = value;
            }
        }

      

        public List<SettingsModel.standardMode> StandardSettings { get => _settingsModel.StandardSettings;  }

        private string _theme;
        public string Theme
        {
            get => _settingsModel.Theme;
            set { SetProperty(ref _theme, value); _settingsModel.Theme = value; }
        }
        public List<string> Themes { get => _settingsModel.Themes;  }

        Sweeper.Models.SettingsModel _settingsModel;
        ISettings set;
        public SettingsPageViewModel(INavigationService navigationService, ISettings settings) : base (navigationService)
        {
            set = settings;

           // _settingsModel = App.Current.Container.Resolve<SettingsModel>();
           // _settingsModel.PropertyChanged += _settingsModel_PropertyChanged;
           // _settingsModel.Load();

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
