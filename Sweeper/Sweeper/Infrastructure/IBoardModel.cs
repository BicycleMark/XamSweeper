using Sweeper.Infrastructure;
using Sweeper.Models.Game;
using System.Collections.ObjectModel;


namespace Sweeper.Models
{
    public interface IBoardModel
    {
        GamePieceModel this[int r, int c] { get; set; }
        int Columns { get; }
        ObservableCollection<GamePieceModel> Model { get; }
        int Rows { get; }
        bool Play(GridPoint gp);
        void Resize(ISettingsModel settings);
        void Save();
    }
}