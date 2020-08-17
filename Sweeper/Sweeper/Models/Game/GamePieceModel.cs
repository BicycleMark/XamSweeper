using Prism.Mvvm;
using Sweeper.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Sweeper.Models.Game
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
            WRONGCHOICE,
            MINE,

            // Following Values indicate the Item is not yet played

            BLANK,
            BUTTON,
            PRESSED,
            FLAGGED
        }

        public bool IsPlayed { get
            {
                bool retVal = (int)_shownValue <= (int)PieceValues.BLANK;
                return retVal;
            }
        }

        public bool IsFlagged
        {
            get { return ShownValue == PieceValues.FLAGGED; }
        }

        private PieceValues _itemValue;
        public PieceValues ItemValue
        {
            get { return _itemValue; }
            set { _itemValue = value; }
        }

        private PieceValues _shownValue;

        public PieceValues ShownValue
        {
            get { return _shownValue; }
            set {
                SetProperty(ref _shownValue,
                            value,
                            notifyRelatedProperties);
            }
        }

        public string Name
        {
            get { return $"[{GridPoint.R},{GridPoint.C}]"; }
        }

        private void notifyRelatedProperties()
        {
            if (_shownValue == PieceValues.FLAGGED)
            {
                RaisePropertyChanged(nameof(IsFlagged));
            }

            if (ShownValue <= PieceValues.BLANK )
            {
                RaisePropertyChanged(nameof(IsPlayed));
            }
        }
   
        private GridPoint  _gridPoint;
        public GridPoint GridPoint 
        {
            get { return _gridPoint; }
            private set { _gridPoint = value; }
        }

        public GamePieceModel(int r, int c)
        {
            GridPoint = new GridPoint(r,c);
            ShownValue = PieceValues.BUTTON;
            ItemValue = PieceValues.NOMINE;
        }

        public GamePieceModel.PieceValues ToggleFlag()
        {
            if (!IsPlayed)
            {
                switch (_shownValue)
                {
                    case (PieceValues.BUTTON):
                        {
                            ShownValue = PieceValues.FLAGGED;
                            break;
                        }

                    case (PieceValues.FLAGGED):
                        {
                            ShownValue = PieceValues.BUTTON;
                            break;
                        }
                    default:
                        break;
                }
            }
            return ShownValue;
        }
    }
}
