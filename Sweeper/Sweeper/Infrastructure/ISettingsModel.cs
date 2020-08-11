using Sweeper.Models;
using System.Collections.Generic;

namespace Sweeper.Infrastructure
{
    public interface ISettingsModel
    {
        int Rows { get; }
        int Columns { get; }
        GameTypes SelectedGameType { get; set; }
        int CurrentThemeIndex { get; set; }
        string CurrentTheme { get;  }
        int CustomColumns { get; set; }
        int CustomMines { get; set; }
        int CustomRows { get; set; }
        int MineCount { get; }

        bool DisableTimerUpdatesForTesting { get; }
        
        List<string> ThemeNames { get; }
        bool Load();
        bool Save();
        
    }
}