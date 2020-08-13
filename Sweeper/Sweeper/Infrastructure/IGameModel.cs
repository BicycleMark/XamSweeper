using Sweeper.Models;
using Sweeper.Models.Game;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Sweeper.Infrastructure
{
    public interface IGameModel
    {
        IBoardModel Board { get; }
        IGameModel Game { get; }
        ObservableCollection<GamePieceModel> Model { get; }
        ISettingsModel Settings { get; set; }  
        GameStates GameState { get; set; }
        int GameTime { get; set; }
        int MineCount { get;  }
        int RemainingMines { get; }
        IPropertyRepository Repo { get; set; }
        bool Disposed { get; }
        void Dispose();
        GameStates ToggleFlag(int r, int c);
        GameStates Play(int r, int c);
    }
}
