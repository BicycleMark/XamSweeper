using Sweeper.Infrastructure;
using System.Collections.ObjectModel;

namespace Sweeper.Models.Game
{
    public interface ISweeperGameModel
    {
        GamePieceModel this[int r, int c] { get; set; }
        bool AllCorrectlyFlagged { get; }
        IBoardModel Board { get; }
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
        void Save();
        GamePieceModel.PieceValues ToggleFlag(GridPoint gp);
        GameStates ToggleFlag(int r, int c);
    }
}