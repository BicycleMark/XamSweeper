using Sweeper.Models;
using System.Collections.Generic;

namespace Sweeper.Infrastructure
{
    public interface ISettingsModel
    {
        int Rows { get; }
        int Columns { get; }
        // GameTypes SelectedGameType { get; set; }
        List<GameDefintion> GameDefinitions { set; get; }
        GameDefintion SelectedGameDefinition { get; set; }

        List<string> Themes { get; set; }
        string CurrentTheme { get; set; }
        int CustomColumns { get; set; }
        int CustomMines { get; set; }
        int CustomRows { get; set; }
        int MineCount { get; }

        bool DisableTimerUpdatesForTesting { get; }
        
       
        bool Load();
        bool Save();
        
    }
}