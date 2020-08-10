using Sweeper.Infrastructure;

namespace Sweeper.Models.Game
{
    public interface IGameModel
    {
        IBoardModel Board { get; set; }
        Infrastructure.IGameModel Game { get; set; }
        GameModel.GameStates GameState { get; set; }
        int GameTime { get; set; }
        int MineCount { get; set; }
        IPropertyRepository Repo { get; set; }

        void Dispose();
        void Flag(int r, int c);
        GameModel.GameStates Play(int r, int c);
    }
}