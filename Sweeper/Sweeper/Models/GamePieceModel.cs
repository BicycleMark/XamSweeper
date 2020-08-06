using Prism.Mvvm;
using Sweeper.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Sweeper.Models
{
    public struct GridPoint
    {
        public int R, C;
        public GridPoint(int r, int c)
        {
            R = r;
            C = c;
        }
    }

    public class GamePieceModel : BindableBase
    {       
        public enum PieceValues
        {
            NOMINE,
            ONEMINE,
            TWOMINE,
            THREEMINE,
            FOURMINE,
            FIVEMINE,
            SIXMINE,
            SEVENMINE,
            EIGHTMINE,
            BLANK,
            BUTTON,
            PRESSED,
            FLAGGED,
            WRONGCHOICE,
            MINE
        }
        private bool  _isPlayed;

        public bool  IsPlayed
        {
            get { return _isPlayed; }
            set { SetProperty(ref _isPlayed, value); }
        }
        
        public bool IsFlagged
        {
            get { return ShownValue == PieceValues.FLAGGED; }
        }

        private PieceValues _itemValue;
        public PieceValues ItemValue
        {
            get { return _itemValue; }
            set {_itemValue =  value; }
        }

        private PieceValues _shownValue;

        public PieceValues ShownValue
        {
            get { return _shownValue; }
            set { SetProperty(ref _shownValue, value); }
        }

        private GridPoint  _gridPoint;
        public GridPoint GridPoint 
        {
            get { return _gridPoint; }
            set { _gridPoint = value; }
        }

        public GamePieceModel(int r, int c)
        {
            GridPoint = new GridPoint(r,c);
            _isPlayed = false;
            ShownValue = PieceValues.BUTTON;
            ItemValue = PieceValues.NOMINE;
        }

        public void ToggleFlag()
        {
            if (_shownValue == PieceValues.BUTTON)
            {          
                ShownValue = PieceValues.FLAGGED;
            }else
            {
                ShownValue = PieceValues.BUTTON;
            }
        }
    }
}
