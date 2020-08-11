using Prism.Navigation;
using Sweeper.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;

namespace Sweeper.Models.Game
{
    public class GameModel : BaseModel, IDisposable, IGameModel
    {
        
        private IBoardModel _board;
        public IBoardModel Board
        {
            get { return _board; }
            set { SetProperty(ref _board, value); }
        }

        private ISettingsModel _settings;
        public ISettingsModel Settings
        {
            get { return _settings; }
            set { SetProperty(ref _settings, value); }
        }

        private IPropertyRepository _repo;
        public IPropertyRepository Repo
        {
            get { return _repo; }
            set { SetProperty(ref _repo, value); }
        }

        private Timer _timer;

        private int _mineCount;

        public int MineCount
        {
            get { return _mineCount; }
            set { _mineCount = value; }
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!Settings.DisableTimerUpdatesForTesting)
                GameTime += 1;
        }

        private int _gameTime;
        public int GameTime
        {
            get { return _gameTime; }
            set
            {
                
                    SetProperty(ref _gameTime, value);
            }
        }

        private GameStates _gameState = GameStates.NOT_STARTED;

        public GameStates GameState
        {
            get { return _gameState; }
            set { SetProperty(ref _gameState, value, OnGameStateChanged); }
        }

        public GameStates Play(int r, int c)
        {
            if (GameState == GameStates.IN_PLAY || GameState == GameStates.NOT_STARTED)
            {
                if (Board.Play(new GridPoint(r, c)))
                {
                    GameState = EvaluateGameState();
                }
                else
                {
                    // You hit a mine
                    GameState = GameStates.LOST;
                }
            }else
            {
                throw new InvalidOperationException(
                    Resources.Sweeper.InvalidGamePlayOperationMustBeInGameStateNOT_STARTED_OR_INPLAY);
            }
            return GameState;
        }

        public GameStates ToggleFlag(int r, int c)
        {
            var retVal = GameStates.IN_PLAY;
            if (GameState == GameStates.IN_PLAY)
            {
                Board[r, c].ToggleFlag();
                retVal = EvaluateGameState();
            }
            return retVal;
        }

        private GameStates EvaluateGameState()
        {
            var retVal = GameState;

            if (GameState == GameStates.NOT_STARTED)
            {
                retVal = GameStates.IN_PLAY;
            }
            else
            {
                if (GameState == GameStates.IN_PLAY)
                {
                    if (GameTime >= 999)
                    {
                        retVal = GameStates.LOST;
                    }
                    else
                    {
                        if (Board.AllCorrectlyFlagged)
                        {
                            retVal = GameStates.WON;
                        }
                    }
                }
            }
            return retVal;
        }

        private void OnGameStateChanged()
        {
            switch (_gameState)
            {
                case GameStates.IN_PLAY:
                    _timer.Enabled = true;
                    
                    GameTime = Settings.DisableTimerUpdatesForTesting ? GameTime : 1;
                    break;
                case GameStates.LOST:
                case GameStates.WON:
                    _timer.Enabled = false;
                    break;
            }
        }

        public GameModel(IPropertyRepository repo, ISettingsModel settings, IBoardModel board) : base(repo)
        {
            _timer = new Timer(1000);
            _timer.Elapsed += _timer_Elapsed;
            
            Settings = settings;
            Repo = repo;
            Board = board;
        }

        public bool Disposed
        {
            get { return _disposed; }
        }

        bool _disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _timer.Stop();
                _timer.Elapsed -= _timer_Elapsed;
                _timer.Dispose();
            }
            _disposed = true;
        }   
    }
}
