using Prism.Navigation;
using Sweeper.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Xamarin.Forms;

namespace Sweeper.Models
{
    public class SettingsModel : BaseModel, ISettings
    {
        public enum GameTypes
        {
            BEGINNER,
            INTERMEDIATE,
            ADVANCED,
            CUSTOM
        }

        private GameTypes _gameType = GameTypes.BEGINNER;

        public GameTypes GameType
        {
            get { return _gameType; }
            set { SetProperty(ref _gameType, value); }
        }

        private string _theme = "Default";
        public string Theme
        {
            get { return _theme; }
            set { SetProperty(ref _theme, value); }
        }

        private int _mineCount;
        public int MineCount
        {
            get { return _mineCount; }
            set { SetProperty(ref _mineCount, value); }
        }

        private int _rows;
        public int Rows
        {
            get { return _rows; }
            set { SetProperty(ref _rows, value); }
        }

        private int _columns;
        public int Columns
        {
            get { return _columns; }
            set { SetProperty(ref _columns, value); }
        }

        public List<standardMode> StandardSettings
        {
            get => _standardSettings;
            set { SetProperty(ref _standardSettings, value); }
        }

        public List<string> Themes
        {
            get { return _themes; }
            set { SetProperty(ref _themes, value); }
        }

        public SettingsModel(IPropertyRepository repo, int nMines, int rows, int columns) :base (repo)
        {
            this.GameType = GameTypes.CUSTOM;
        }

        public struct standardMode
        {
            public int rows, cols, mines;
            public standardMode(int r, int c, int m)
            {
                rows = r;
                cols = c;
                mines = m;
            }
        }
        public bool Save()
        {
            return true;
        }

        public bool Load()
        {
            return true;

        }
        private static List<string> _themes;
        
        private static List<standardMode> _standardSettings;
        static SettingsModel()
        {
            var str = Resources.Resources.Themes;
            _themes = new List<string>() { "Default", "Chocolate", "Copper", "Key West", "Powder Puff" };
            _standardSettings = new List<standardMode>()
            {
                new standardMode(10, 10, 10),
                new standardMode(20, 20, 20),
                new standardMode(40, 40, 40)
            };
        }

        public SettingsModel(IPropertyRepository repo) : base(repo)
        {


                GameType = GameTypes.BEGINNER;
                int ndx = (int)GameType;
                Rows = _standardSettings[ndx].rows;
                Columns = _standardSettings[ndx].cols;
                MineCount = _standardSettings[ndx].mines;
           
        }
    }
}
