using Sweeper.Infrastructure;
using Sweeper.Models.Game;
using System.Collections.ObjectModel;


namespace Sweeper.Infrastructure
{
    public interface IBoardModel
    {
        GamePieceModel this[int r, int c] { get; set; }
        bool AllCorrectlyFlagged { get;  }
        int Columns { get; }
        ObservableCollection<GamePieceModel> Model { get; }
        int Rows { get; }
        int Mines { get; }
        bool Play(GridPoint gp);
        PieceValues ToggleFlag(GridPoint gp);
        void Resize(ISettingsModel settings);
       
    }
}