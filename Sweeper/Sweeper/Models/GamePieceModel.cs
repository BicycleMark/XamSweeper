using Sweeper.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Sweeper.Models
{
    public struct GridPoint
    {
        public int X, Y;
        public GridPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class GamePieceModel : BaseModel
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

        public GamePieceModel(int x, int y)
        {
            GridPoint = new GridPoint(x, y);
            _isPlayed = false;
            ShownValue = PieceValues.BUTTON;
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
