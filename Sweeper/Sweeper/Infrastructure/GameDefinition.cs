using System;
using System.Collections.Generic;
using System.Text;

namespace Sweeper.Infrastructure
{
    public enum GameTypes
    {
        BEGINNER,
        INTERMEDIATE,
        ADVANCED,
        CUSTOM
    }
    public struct GameDefintion
    {

        public string Name { get; set; }
        public GameTypes Type { get; set; }
        public int Rows, Cols, Mines;
        public GameDefintion(GameTypes type, int r, int c, int m, string name = null)
        {
            Type = type;
            Name = name;
            Rows = r;
            Cols = c;
            Mines = m;
        }
    }
}

