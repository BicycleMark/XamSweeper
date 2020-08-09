using Sweeper.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sweeper.Models
{
    public class SettingsModel : BaseModel, ISettingsModel
    {

        public SettingsModel(IPropertyRepository repo) : base(repo)
        {
            ThemeNames = new List<string>();
            ThemeNames = Resources.Sweeper.Themes.Split(',').ToList<string>();

            GameDefinitions = new ObservableCollection<GameDefintion>();
            var gameDefString = Resources.Sweeper.GameTypeDefs;
            var defs = gameDefString.Split('|');
            foreach (var def in defs)
            {
                for (GameTypes gt = GameTypes.BEGINNER; gt <= GameTypes.CUSTOM; gt++)
                {
                    var fields = def.Split(',');
                    if (fields.Length != 4)
                    {
                        throw new Exception(Resources.Sweeper.ExceptionGameDefsMustHave4Definitions);
                    }
                    else
                    {

                        
                        this.GameDefinitions.Add(new GameDefintion(gt,
                                                                    r: Int32.Parse(fields[1]),
                                                                    c: Int32.Parse(fields[2]),
                                                                    m: Int32.Parse(fields[3]),
                                                                    name: fields[0]));

                        
                    }
                }
            }

            SelectedGameType = GameTypes.BEGINNER;
            CurrentThemeIndex = 0;

        }

        private int _currentThemeIndex = 0;
        public int CurrentThemeIndex
        {
            get { return _currentThemeIndex; }
            set { SetProperty(ref _currentThemeIndex, value, CurrentThemeIndexChanged); ; }
        }

        private void CurrentThemeIndexChanged()
        {
            RaisePropertyChanged(nameof(CurrentTheme));
        }

        List<string> _themeNames;
        public List<string> ThemeNames
        {
            get => _themeNames;
            private set => _themeNames = value;
        }

        public string CurrentTheme
        {
            get { return _themeNames.ToArray()[_currentThemeIndex]; }
        }



        #region GameTypes

        ObservableCollection<GameDefintion> _gameDefinitions;
        public ObservableCollection<GameDefintion> GameDefinitions
        {
            get { return _gameDefinitions; }
            set { SetProperty(ref _gameDefinitions, value); }
        }

        private GameTypes _gameType = GameTypes.BEGINNER;
        GameTypes _selectedGameType;
        public GameTypes SelectedGameType
        {
            get { return _selectedGameType; }
            set { SetProperty(ref _selectedGameType, value, SetGameType); }
        }

        private void SetGameType()
        {
            RaisePropertyChanged(nameof(MineCount));
            RaisePropertyChanged(nameof(Rows));
            RaisePropertyChanged(nameof(Columns));
            if (_selectedGameType == GameTypes.CUSTOM)
            {
                this.CustomMines = this.GameDefinitions[(int)GameTypes.CUSTOM].Mines;
                this.CustomRows = this.GameDefinitions[(int)GameTypes.CUSTOM].Rows;
                this.CustomColumns = this.GameDefinitions[(int)GameTypes.CUSTOM].Cols;
            }
        }

        public int MineCount
        {
            get { return GameDefinitions[(int)_selectedGameType].Mines; }
        }

        public int Rows
        {
            get { return GameDefinitions[(int)_selectedGameType].Rows; }
        }

        public int Columns
        {
            get { return GameDefinitions[(int)_selectedGameType].Cols; }
        }
        #endregion

     
        public bool Save()
        {
            return true;
        }

        public bool Load()
        {
            return true;

        }

        int _customMines;
        public int CustomMines
        {
            get { return _customMines; }
            set { SetProperty(ref _customMines, value); }
        }

        int _customRows;
        public int CustomRows
        {
            get { return _customRows; }
            set { SetProperty(ref _customRows, value); }
        }

        int _customColumns;
        public int CustomColumns
        {
            get { return _customColumns; }
            set { SetProperty(ref _customColumns, value); }
        }   
    }
}
