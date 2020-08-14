using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Prism;
using Prism.Ioc;

namespace Sweeper.Droid
{
    [Activity(Label = "Sweeper", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Xamarin.Essentials.Platform.Init(this, bundle); // add this line to your code, it may also be called: bundle

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    
    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
            // Register any platform specific implementations
        }
    }
}

/*
 * Severity	Code	Description	Project	File	Line	Suppression State
Error		Could not find 39 Android X assemblies, make sure to install the following NuGet packages:
 - Xamarin.AndroidX.Lifecycle.LiveData
 - Xamarin.AndroidX.Browser
 - Xamarin.Google.Android.Material
 - Xamarin.AndroidX.Legacy.Support.V4
 - Xamarin.AndroidX.MediaRouter
You can also copy-and-paste the following snippet into your .csproj file:
    <PackageReference Include="Xamarin.AndroidX.Lifecycle.LiveData" Version="2.2.0.1" />
    <PackageReference Include="Xamarin.AndroidX.Browser" Version="1.2.0.1" />
    <PackageReference Include="Xamarin.Google.Android.Material" Version="1.1.0.1-rc3" />
    <PackageReference Include="Xamarin.AndroidX.Legacy.Support.V4" Version="1.0.0.1" />
    <PackageReference Include="Xamarin.AndroidX.MediaRouter" Version="1.1.0.1" />	Sweeper.Android			

 */

