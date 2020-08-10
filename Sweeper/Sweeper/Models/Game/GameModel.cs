using Prism.Navigation;
using Sweeper.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private IGameModel _game;
        public IGameModel Game
        {
            get { return _game; }
            set { SetProperty(ref _game, value, SetGameComponents); }
        }

        private IPropertyRepository _repo;
        public IPropertyRepository Repo
        {
            get { return _repo; }
            set { SetProperty(ref _repo, value); }
        }


        private void SetGameComponents()
        {
            Board = _game.Board;
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
            GameTime += 1;
        }

        private int _gameTime;
        public int GameTime
        {
            get { return _gameTime; }
            set => SetProperty(ref _gameTime, value);
        }

        private GameStates _gameState = GameStates.NOT_STARTED;

        public GameStates GameState
        {
            get { return _gameState; }
            set { SetProperty(ref _gameState, value, OnGameStateChanged); }
        }

        public GameStates Play(int r, int c)
        {
            return GameStates.IN_PLAY;
        }

        public void Flag(int r, int c)
        {

        }
        private void OnGameStateChanged()
        {
            switch (_gameState)
            {
                case GameStates.IN_PLAY:
                    _timer.Enabled = true;
                    break;
                case GameStates.LOST:
                case GameStates.WON:
                    _timer.Enabled = false;
                    break;
            }
        }

        public GameModel(IPropertyRepository repo, IGameModel game) : base(repo)
        {
            _timer = new Timer(1000);
            _timer.Elapsed += _timer_Elapsed;
            Repo = repo;
            Game = game;
        }

        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _timer.Stop();
                _timer.Elapsed -= _timer_Elapsed;
                _timer.Dispose();
            }
            disposed = true;
        }

      
    }
}
