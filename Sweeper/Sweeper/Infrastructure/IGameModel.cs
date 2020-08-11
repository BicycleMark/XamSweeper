using Sweeper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sweeper.Infrastructure
{
    public interface IGameModel
    {
        IBoardModel Board { get; set; }
        ISettingsModel Settings { get; set; }
       
        GameStates GameState { get; set; }
        int GameTime { get; set; }
        int MineCount { get; set; }
        IPropertyRepository Repo { get; set; }

        void Dispose();
        GameStates ToggleFlag(int r, int c);
        GameStates Play(int r, int c);
    }
}
