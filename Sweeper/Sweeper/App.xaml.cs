﻿using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sweeper.Views;
using Sweeper.ViewModels;
using Sweeper.Models;
using Prism.Unity;
using Unity;
using Sweeper.Infrastructure;
using Unity.Lifetime;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Sweeper
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        private IContainerRegistry _containerRegistry;
        //public IContainerRegistry ContainerRegistry
        //{
        //    get { return _containerRegistry; }
        //    private set {_containerRegistry = value; }
        //}
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
         
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

           // ContainerRegistry = containerRegistry;
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<GamePage, GamePageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            containerRegistry.RegisterSingleton<SettingsModel>();
            var instance = new SettingsModel();

            this.Container.GetContainer().RegisterInstance<ISettingsModel>(instance);
            var s = Container.GetContainer().Resolve<ISettingsModel>();
           

        }

        private static IInstanceLifetimeManager GetSingleton()
        {
            return InstanceLifetime.Singleton;
        }
    }
}
