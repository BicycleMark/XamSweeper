using Sweeper.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Sweeper.Models
{
    public class SettingsModel : BaseModel, ISettingsModel
    {
        
        public SettingsModel(IPropertyRepository repo, ISettingsProvider provider) : base(repo)
        {
            if (provider == null || String.IsNullOrEmpty(provider.DefinitionsSource) || string.IsNullOrEmpty(provider.ThemeSource ))
            {
                throw new ArgumentException("provider cannot be null");
            }    
            Themes = new List<string>();
           
            Themes = provider.ThemeSource.Split(',').ToList<string>();
            if (Themes.Count < 2)
            {
                throw new ArgumentException(Resources.Sweeper.ExceptionGameDefsMustHave4Definitions);
            }

            GameDefinitions = new List<GameDefintion>();
            //var gameDefString = Resources.Sweeper.GameTypeDefs;
            var gameDefString = provider.DefinitionsSource;
          

            var defs = gameDefString.Split('|');
            foreach (var def in defs)
            {
                {
                    var fields = def.Split(',');
                    if (fields.Length != 4)
                    {
                        throw new ArgumentException (Resources.Sweeper.ExceptionGameDefsMustHave4Definitions);
                    }
                    else
                    {           
                        this.GameDefinitions.Add(new GameDefintion(type: (GameTypes)Enum.Parse(typeof(GameTypes), fields[0]),
                                                                      r: Int32.Parse(fields[1]),
                                                                      c: Int32.Parse(fields[2]),
                                                                      m: Int32.Parse(fields[3]),
                                                                   name: fields[0]));   
                    }                 
                }      
            }
            SelectedGameDefinition = GameDefinitions.First();
            CurrentTheme = "Default";
            var custom = GameDefinitions.FirstOrDefault(m => m.Type == GameTypes.CUSTOM);
            CustomMines = custom.Mines;
            CustomRows = custom.Rows;
            CustomColumns = custom.Columns;
        }

        List<string> _themes;
        public List<string> Themes
        {
            get {return _themes; }
            set { SetProperty(ref _themes, value); }
        }

        private string _currentTheme;
        public string CurrentTheme
        {
            get { return _currentTheme; }
            set { SetProperty(ref _currentTheme, value); }
        }

        #region GameTypes
        List<GameDefintion> _gameDefinitions;
        public List<GameDefintion> GameDefinitions
        {
            get { return _gameDefinitions; }
            set { SetProperty(ref _gameDefinitions, value); }
        }

        private GameDefintion _selectedGameDefinition;
        public GameDefintion SelectedGameDefinition
        {
            get { return _selectedGameDefinition; }
            set { SetProperty(ref _selectedGameDefinition, value); }
        }

      

        public int MineCount
        {
            get {
                return    
                  SelectedGameDefinition.Mines;
            }
        }

        public int Rows
        {
            get {
                return SelectedGameDefinition.Rows;
            }
        }

        public int Columns
        {
            get {
                return SelectedGameDefinition.Columns;            
            }
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

        private bool _disableTimerUpdatesForTesting = false;
        public bool DisableTimerUpdatesForTesting 
        {
            get { return _disableTimerUpdatesForTesting; }
        }
    }
}
