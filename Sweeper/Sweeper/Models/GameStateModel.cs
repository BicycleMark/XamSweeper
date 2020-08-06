using Sweeper.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Sweeper.Models
{
    public class GameStateModel : BaseModel
    {  
        private Timer _timer; 
        public enum GameStates
        {
            IN_PLAY,
            NOT_STARTED,
            WON,
            LOST
        }

        private GameStates _gameState;
        public GameStates GameState
        {
            get { return _gameState; }
            set { SetProperty(ref _gameState, 
                              value, 
                              ()=> { _timer.Enabled = _gameState == GameStates.IN_PLAY; }  ); 
               }
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
            _timer = new Timer(1000);
            var maxTime = System.Convert.ToInt32(Resources.Sweeper.GameMaxTime);
            _timer.Elapsed += (s, e) =>
            {
                _gameTime += 1;
                if (_gameTime > maxTime)
                {
                    GameState = GameStates.LOST;
                }
            };
        }  
    }
}
