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
    public class SettingsModel : BaseModel
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
            private set { SetProperty(ref _gameType, value); }
        }

        private string _theme = "Default";
        public string Theme
        {
            get { return _theme; }
            set { SetProperty (ref _theme, value); }
        }

        private int _mineCount;
        public int MineCount
        {
            get { return _mineCount; }
            private set { SetProperty(ref _mineCount, value); }
        }

        private int _rows;
        public int Rows
        {
            get { return _rows; }
            private set { SetProperty(ref _rows, value) ; }
        }

        private int _columns;
        public int Columns
        {
            get { return _columns; }
            private set { SetProperty(ref _columns, value); }
        }

        public  List<standardMode> StandardSettings 
        {   get => _standardSettings; 
            set { SetProperty(ref _standardSettings, value); } 
        }

        public List<string> Themes
        {
            get { return _themes; }
            set { SetProperty(ref _themes, value); }
        }

        public SettingsModel(int nMines, int rows, int columns )
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

        }

        public bool Load()
        {

        }
        private static List<string> _themes;
        private static List<standardMode> _standardSettings;
        static SettingsModel()
        {
            _themes = new List<string>() { "Default", "Chocolate", "Copper", "Key West", "Powder Puff" };
            _standardSettings = new List<standardMode>() 
            { 
                new standardMode(10, 10, 10), 
                new standardMode(20, 20, 20), 
                new standardMode(40, 40, 40) 
            };
        }

        public SettingsModel(GameTypes gameType)
        {

            if (gameType == GameTypes.CUSTOM)
            {
                throw new ArgumentException(
                    "When creating a Custon Game you must use alternate "+
                    "constructor SettingsModel(int nMines, int rows, int columns )"
                );

            }else
            {
                GameType = gameType;
                int ndx = (int)gameType;
                Rows = _standardSettings[ndx].rows;
                Columns = _standardSettings[ndx].cols;
                MineCount = _standardSettings[ndx].mines;
            }
        }
    }
}
