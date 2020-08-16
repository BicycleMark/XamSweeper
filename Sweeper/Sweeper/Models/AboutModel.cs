using Prism.Mvvm;
using Prism.Navigation;
using Sweeper.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sweeper.Models
{
   
    public class AboutModel : BindableBase, IAboutModel
    {
        public IAppInfo AppInfo { get; private set; }

        public AboutModel(IAppInfo appInfo)
        {
            AppInfo = appInfo;

        }
    }
}
