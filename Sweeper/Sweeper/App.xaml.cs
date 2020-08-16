using Prism;
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
using System.Threading.Tasks;
using Sweeper.Models.Game;
using Xamarin.Essentials;

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
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<GamePage, GamePageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<AboutPage, AboutPageViewModel>();

            var repo = new XamarinEssentialsPropertyRepo();
            var provider = new ResouceSettingsProvider();
            var settings = new SettingsModel(repo,provider);
            var appInfo = new XamarinEsentialsAppInfo();  
            var sgm = new SweeperGameModel(repo, settings, false);
            var aboutmodel = new AboutModel(appInfo);

            containerRegistry.GetContainer().RegisterInstance<ISettingsProvider>(provider);
            containerRegistry.GetContainer().RegisterInstance<IAppInfo>(appInfo);
            containerRegistry.GetContainer().RegisterInstance<IAboutModel>(aboutmodel);
            containerRegistry.GetContainer().RegisterInstance<ISettingsModel>(settings);
            containerRegistry.GetContainer().RegisterInstance<ISweeperGameModel>(sgm);
        }

        private static IInstanceLifetimeManager GetSingleton()
        {
            return InstanceLifetime.Singleton;
        }    
    }
}
