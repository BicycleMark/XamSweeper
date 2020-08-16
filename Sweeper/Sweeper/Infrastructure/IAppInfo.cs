using System;
using System.Collections.Generic;
using System.Text;

namespace Sweeper.Infrastructure
{
    public interface IAppInfo
    { 
        string PackageName { get; }
        string Name { get; }
        string VersionString { get; }
        Version Version { get; }
        string BuildString { get; }
        void ShowSettingsUI();    
    }
}
