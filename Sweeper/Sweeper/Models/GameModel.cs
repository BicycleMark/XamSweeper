using Prism.Navigation;
using Sweeper.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;

namespace Sweeper.Models
{
    public class GameModel : BaseModel, IDisposable
    {

        private ObservableCollection<GamePieceModel> _board;
        public ObservableCollection<GamePieceModel> Board { get => _board; private set => _board = value; }

        private Timer _timer;
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

        public GameModel(SettingsModel settingModel)
        {
            _timer = new Timer(1000);
            _timer.Elapsed += _timer_Elapsed;
            _board = new ObservableCollection<GamePieceModel>();
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
