using System;
using System.Collections.Generic;
using System.Text;

namespace Sweeper.Infrastructure
{
    public class ResouceSettingsProvider : ISettingsProvider
    {
        public string ThemeSource { get { return Resources.Sweeper.Themes; } }

        public string DefinitionsSource { get { return Resources.Sweeper.GameTypeDefs; } }
    }
}
