using Sweeper.Models;
using System.Collections.Generic;

namespace Sweeper.Infrastructure
{
    public interface ISettings
    {
        int Columns { get; set; }
        SettingsModel.GameTypes GameType { get; set; }
        int MineCount { get; set; }
        int Rows { get; set;  }
        List<SettingsModel.standardMode> StandardSettings { get; set; }
        string Theme { get; set; }
        List<string> Themes { get; set; }

        bool Load();
        bool Save();
    }
}