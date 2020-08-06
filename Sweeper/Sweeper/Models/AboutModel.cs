using Prism.Mvvm;
using Prism.Navigation;
using Sweeper.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sweeper.Models
{
    public class AboutModel : BindableBase
    {
        public Version Version { get; }
        public AboutModel() 
        {
           

        }
    }
}
