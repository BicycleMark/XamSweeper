using Sweeper.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sweeper.Models
{
    public class GameStateModel : BaseModel
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

        private GameStates _gameState;
        public GameStates GameState
        {
            get { return _gameState; }
            set { SetProperty(ref _gameState, value); }
        }
        private int _gameTime;
        public int GameTime
        {
            get { return _gameTime; }
            set { SetProperty(ref _gameTime, value);  }
        }

        private int _remainingMines;

        public int RemainingMines
        {
            get { return _remainingMines; }
            set { SetProperty(ref _remainingMines,value); }
        }




        public GameStateModel(IPropertyRepository repo, bool loadFromRepo) : base(repo)
        {


        }
    }
}
