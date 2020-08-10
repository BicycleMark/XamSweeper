using System;
using System.Collections.Generic;
using System.Text;

namespace Sweeper.Infrastructure
{
    public enum GameStates
    {
        NOT_DETERMINED,
        NOT_STARTED,
        IN_DECISION,
        IN_PLAY,
        IN_BONUSPLAY,
        WON,
        LOST
    }
}
