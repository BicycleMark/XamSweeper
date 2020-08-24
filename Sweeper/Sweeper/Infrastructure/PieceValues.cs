using System;
using System.Collections.Generic;
using System.Text;

namespace Sweeper.Infrastructure
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
}
