using System;
using System.Collections.Generic;
using System.Text;

namespace Sweeper.Infrastructure
{
    public interface ISettingsProvider
    {
        string ThemeSource
        {
            get;
        }

        string DefinitionsSource
        {
            get;
        }
    }
}
