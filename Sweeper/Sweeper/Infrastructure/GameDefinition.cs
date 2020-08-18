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
    public class GameDefintion
    {

        public string Name { get; set; }
        public GameTypes Type { get; set; }
        public int Rows { get; set; }
        public int Columns {get; set;}
        public int Mines{get;set;}
        public GameDefintion(GameTypes type, int r, int c, int m, string name = null)
        {
            Type = type;
            Name = name;
            Rows = r;
            Columns = c;
            Mines = m;
        }
    }
}

