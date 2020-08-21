using System;
using Prism;
using Prism.Commands;
using Prism.Navigation;
using Sweeper.Infrastructure;

namespace Sweeper.ViewModels
{
    public class GamePageViewModel : AppMapViewModelBase, IActiveAware
    {
#pragma warning disable 67
        public event EventHandler IsActiveChanged;
#pragma warning restore 67

        public bool IsActive { get; set; }
       

        private IBoardModel _board;
        public IBoardModel Board { get => _board; 
                                   set => SetProperty(ref _board, value); 
                                 }

        public DelegateCommand<string> PlayComand { get; private set; }

        private ISettingsModel _settings;
        public ISettingsModel Settings { get => _settings; 
                                    set => SetProperty(ref _settings, value); 

                                  }
        private IGameModel _game;
        public IGameModel Game
        {
            get => _game;
            set => SetProperty(ref _game, value);
        }
       

        private int pieceSeparator = 2;
        public int PieceSeparator { get => pieceSeparator; set => SetProperty(ref pieceSeparator, value); }


        public GamePageViewModel(INavigationService navigationService,
                                 ISettingsModel settingsModel,
                                 ISweeperGameModel sweeperGameModel) :
                                 base(navigationService)
        {
            Settings = settingsModel;
            Board = sweeperGameModel.Board;
            Game = sweeperGameModel.Game;
            PlayComand = new DelegateCommand<string>(ParseAndPlay, CanPlayCommand);
        }

        private (int r, int c) Parse(string strToParse)
        {
            if (strToParse == null)
                return(-1,-1);
            string[] parsed = strToParse.Split(',');
            return (int.Parse(parsed[0]), int.Parse(parsed[1]));
        }
        private bool CanPlayCommand(string arg)
        {
            var v = Parse(arg);
            if (v.r > -1 && v.c > -1)
                return Board[v.r, v.c].IsPlayed;
            else
                return false;
        }

        private void ParseAndPlay(string arg)
        {
            var v = Parse(arg);
            if (v.r > -1 && v.c > -1)
                Game.Play(v.r, v.c);
            else
                return;
        }
    }
}
