using Sweeper.Infrastructure;
using Sweeper.Models.Game;
using System.Collections.ObjectModel;

namespace Sweeper.Infrastructure
{
    public interface ISweeperGameModel
    {
        GamePieceModel this[int r, int c] { get; set; }
        bool AllCorrectlyFlagged { get; }
        IBoardModel Board { get; }
        IGameModel Game { get; }
        int Columns { get; }
        bool Disposed { get; }
        GameStates GameState { get; set; }
        int GameTime { get; set; }
        int MineCount { get; }
        int Mines { get; }
        ObservableCollection<GamePieceModel> Model { get; }
        int RemainingMines { get; }
        IPropertyRepository Repo { get; set; }
        int Rows { get; }
        ISettingsModel Settings { get; set; }
        void Dispose();
        bool Play(GridPoint gp);
        GameStates Play(int r, int c);
        void Resize(ISettingsModel settings);
        
        PieceValues ToggleFlag(GridPoint gp);
        GameStates ToggleFlag(int r, int c);
    }
}