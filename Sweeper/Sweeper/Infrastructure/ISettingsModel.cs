using Sweeper.Models;
using System.Collections.Generic;

namespace Sweeper.Infrastructure
{
    public interface ISettingsModel
    {
        int Columns { get; }
        SettingsModel.GameTypes GameType { get; }
        int MineCount { get; }
        int Rows { get; }
        List<SettingsModel.standardMode> StandardSettings { get; set; }
        string Theme { get; set; }
        List<string> Themes { get; set; }

        bool Load();
        bool Save();
    }
}