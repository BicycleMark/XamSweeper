﻿using Prism.Mvvm;
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
            WRONGCHOICE,
            MINE,

            // Following Values indicate the Item is not yet played

            BLANK,  
            BUTTON,
            PRESSED,
            FLAGGED
        }

        public bool IsPlayed => _shownValue <= PieceValues.WRONGCHOICE;
     
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
            set {
                    SetProperty(ref _shownValue,
                                value,
                                notifyRelatedProperties); 
                }
        }

        private void notifyRelatedProperties()
        {
            RaisePropertyChanged(nameof(IsFlagged));
            RaisePropertyChanged(nameof(IsPlayed));
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

        public void ToggleFlag()
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
    }
}
