using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Sweeper.Infrastructure
{
    public class XamarinEsentialsAppInfo : IAppInfo
    {
        public string PackageName => AppInfo.PackageName;

        public string Name => AppInfo.Name;

        public string VersionString => AppInfo.VersionString;

        public Version Version => AppInfo.Version;

        public string BuildString => AppInfo.BuildString;

        public void ShowSettingsUI()
        {
            throw new NotImplementedException();
        }
    }
}
